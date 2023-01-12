using Dapper;
using EngineerWorld.Model.ForumComment;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Repository
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        private IConfiguration _config;

        public ForumCommentRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int forumCommentId)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            { 
               await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync("ForumComment_Delete",
                    new { ForumCommentId = forumCommentId }, commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<List<ForumComment>> GetAllAsync(int forumId)
        {
            IEnumerable<ForumComment> forumComments;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                forumComments = await connection.QueryAsync<ForumComment>("ForumComment_GetAll",
                    new { ForumId = forumId }, commandType: CommandType.StoredProcedure);
            }

            return forumComments.ToList();

        }

        public async Task<ForumComment> GetAsync(int forumCommentId)
        {
            ForumComment forumComment;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                forumComment = await connection.QueryFirstOrDefaultAsync<ForumComment>("ForumComment_Get",
                                                                                  new { ForumCommentId = forumCommentId },
                                                                                  commandType: CommandType.StoredProcedure);
            }

            return forumComment;
        }

        public async Task<ForumComment> UpsertAsync(ForumCommentCreate forumCommentCreate, int applicationUserId)
        {
            var dataTable  = new DataTable();
            dataTable.Columns.Add("ForumCommentId", typeof(int));
            dataTable.Columns.Add("ParentForumCommentId", typeof(int));
            dataTable.Columns.Add("ForumId", typeof(int));
            dataTable.Columns.Add("Content", typeof(string));



            dataTable.Rows.Add(forumCommentCreate.ForumCommentId, forumCommentCreate.ParentForumCommentId, forumCommentCreate.ForumId, forumCommentCreate.Content);

            int? newForumCommentId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newForumCommentId = await connection.ExecuteScalarAsync<int?>("ForumComment_Upsert",
                                                                              new { ForumComment = dataTable.AsTableValuedParameter("dbo.ForumCommentType"),
                                                                              ApplicationUserId = applicationUserId},
                                                                              commandType: CommandType.StoredProcedure);
            }

            newForumCommentId = newForumCommentId ?? forumCommentCreate.ForumCommentId;

            ForumComment forumComment = await GetAsync(newForumCommentId.Value);

            return forumComment;
        }
    }
}
