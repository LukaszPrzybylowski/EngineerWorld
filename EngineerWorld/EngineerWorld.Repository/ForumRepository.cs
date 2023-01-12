using Dapper;
using EngineerWorld.Model.Article;
using EngineerWorld.Model.Forum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Repository
{
    public class ForumRepository : IForumRepository
    {
        private readonly IConfiguration _config;

        public ForumRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int forumId)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync("Forum_Delete",
                                                             new { ForumId = forumId },
                                                             commandType: CommandType.StoredProcedure);   
            }

            return affectedRows;
        }

        public async Task<ForumPagedResults<Forum>> GetAllAsync(ForumPaging forumPaging)
        {
            var results = new ForumPagedResults<Forum>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var multi = await connection.QueryMultipleAsync("Forum_GetAll",
                    new
                    {
                        Offset = (forumPaging.Page - 1) * forumPaging.PageSize,
                        PageSize = forumPaging.PageSize
                    }, commandType: CommandType.StoredProcedure))
                {
                    results.Items = multi.Read<Forum>();

                    results.TotalCount = multi.ReadFirst<int>();
                }
            }

            return results;

        }

        public async Task<List<Forum>> GetAllByUserIdAsync(int applicationUserId)
        {
            IEnumerable<Forum> forums;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                forums = await connection.QueryAsync<Forum>("Forum_GetByUserId",
                                                           new { ApplicationUserId = applicationUserId },
                                                           commandType: CommandType.StoredProcedure);
            }

            return forums.ToList();
        }

        public async Task<List<Forum>> GetAllFamousAsync()
        {
            IEnumerable<Forum> famousForum;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                famousForum = await connection.QueryAsync<Forum>("Forum_GetAllFamous",
                                                                  new {} , commandType: CommandType.StoredProcedure);
            }

            return famousForum.ToList();
        }

        public async Task<Forum> GetAsync(int forumId)
        {
            Forum forum;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                forum = await connection.QueryFirstOrDefaultAsync<Forum>(
                    "Forum_Get",
                    new { ForumId = forumId },
                    commandType: CommandType.StoredProcedure);
            }

            return forum;
        }

        public async Task<Forum> UpsertAsync(ForumCreate forumCreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("ForumId", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Content", typeof(string));
            dataTable.Columns.Add("PhotoId", typeof(int));

            dataTable.Rows.Add(forumCreate.ForumId, forumCreate.Title, forumCreate.Content, forumCreate.PhotoId);

            int? newForumId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newForumId = await connection.ExecuteScalarAsync<int?>(
                    "Forum_Upsert",
                    new
                    {
                        Forum = dataTable.AsTableValuedParameter("dbo.ForumType"),
                        ApplicationUserId = applicationUserId
                    }, commandType: CommandType.StoredProcedure
                    );
            }

            newForumId = newForumId ?? forumCreate.ForumId;

            Forum forum = await GetAsync(newForumId.Value);

            return forum;
        }
    }
}
