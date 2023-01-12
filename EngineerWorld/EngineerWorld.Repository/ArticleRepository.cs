using Dapper;
using EngineerWorld.Model.Account;
using EngineerWorld.Model.Article;
using EngineerWorld.Model.Photo;
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
    public class ArticleRepository : IArticleRepository
    {
        private readonly IConfiguration _config;

        public ArticleRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<int> DeleteAsync(int articleId)
        {
            
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync(
                    "Article_Delete",
                    new { ArticleId = articleId },
                    commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<ArticlePagedResults<Article>> GetAllAsync(ArticlePaging articlePaging)
        {
            var results = new ArticlePagedResults<Article>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var multi = await connection.QueryMultipleAsync(
                    "Article_GetAll",
                    new
                    {
                        Offset = (articlePaging.Page - 1) * articlePaging.PageSize,
                        PageSize = articlePaging.PageSize
                    },
                    commandType: CommandType.StoredProcedure)) 
                { 
                    results.Items = multi.Read<Article>();

                    results.TotalCount = multi.ReadFirst<int>();
                }
               
            }

            return results;
        }

        public async Task<List<Article>> GetAllByUserIdAsync(int applicationUserId)
        {
            IEnumerable<Article> articles;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                articles = await connection.QueryAsync<Article>("Article_GetByUserId",
                    new { ApplicationUserId = applicationUserId },
                    commandType: CommandType.StoredProcedure);

            }

            return articles.ToList();
        }

        public async Task<List<Article>> GetAllFamousAsync()
        {
            IEnumerable<Article> famousArticles;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                famousArticles = await connection.QueryAsync<Article>("Article_GetAllFamous",
                    new { },
                    commandType: CommandType.StoredProcedure);

            }

            return famousArticles.ToList();
        }

        public async Task<Article> GetAsync(int articleId)
        {
            Article article;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            { 
                await connection.OpenAsync();

                article = await connection.QueryFirstOrDefaultAsync<Article>(
                    "Article_Get",
                    new { ArticleId = articleId },
                    commandType: CommandType.StoredProcedure);
            }

            return article;
        }

        public async Task<Article> UpsertAsync(ArticleCreate articleCreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("ArticleId", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Content", typeof(string));
            dataTable.Columns.Add("PhotoId", typeof(int));

            dataTable.Rows.Add(articleCreate.ArticleId, articleCreate.Title, articleCreate.Content, articleCreate.PhotoId);

            int? newArticleId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newArticleId = await connection.ExecuteScalarAsync<int?>(
                    "Article_Upsert",
                    new
                    {
                        Article = dataTable.AsTableValuedParameter("dbo.ArticleType"),
                        ApplicationUserId = applicationUserId
                    }, commandType: CommandType.StoredProcedure
                    );
            }

            newArticleId = newArticleId ?? articleCreate.ArticleId;

            Article article = await GetAsync(newArticleId.Value);

            return article;

        }
    }
}
