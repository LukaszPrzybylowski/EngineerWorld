using Dapper;
using EngineerWorld.Model.ArticleComment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Repository
{
    public class ArticleCommentRepository : IArticleCommentRepository
    {
        private IConfiguration _config;

        public ArticleCommentRepository(IConfiguration config)
        { 
            _config= config;
        }
        public async Task<int> DeleteAsync(int articleCommentId)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync(
                    "ArticleComment_Delete",
                    new { ArticleCommentId = articleCommentId },
                    commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<List<ArticleComment>> GetAllAsync(int articleId)
        {
            IEnumerable<ArticleComment> articleComments;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                articleComments = await connection.QueryAsync<ArticleComment>("ArticleComment_GetAll",
                    new { ArticleId = articleId },
                    commandType: CommandType.StoredProcedure);
            }

            return articleComments.ToList();
        }

        public async Task<ArticleComment> GetAsync(int articleCommentId)
        {
            ArticleComment articleComment;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                articleComment = await connection.QueryFirstOrDefaultAsync<ArticleComment>("ArticleComment_Get",
                    new { ArticleCommentId = articleCommentId }, 
                    commandType: CommandType.StoredProcedure);
            }

            return articleComment;
        }

        public async Task<ArticleComment> UpsertAsync(ArticleCommentCreate articleCommentCreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("ArticleCommentId", typeof(int));
            dataTable.Columns.Add("ParentArticleCommentId", typeof(int));
            dataTable.Columns.Add("ArticleId", typeof(int));
            dataTable.Columns.Add("Content", typeof(string));

            dataTable.Rows.Add(articleCommentCreate.ArticleCommentId,
                                articleCommentCreate.ParentAticleCommentId,
                                articleCommentCreate.ArticleId,
                                articleCommentCreate.Content);

            int? newArticleCommentId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newArticleCommentId = await connection.ExecuteScalarAsync<int?>("ArticleComment_Upsert",
                    new { ArticleComment = dataTable.AsTableValuedParameter("dbo.ArticleCommentType"),
                    ApplicationUserId = applicationUserId},
                    commandType: CommandType.StoredProcedure);

            }


            newArticleCommentId = newArticleCommentId ?? articleCommentCreate.ArticleCommentId;

            ArticleComment articleComment = await GetAsync(newArticleCommentId.Value);

            return articleComment;
        }
    }
}
