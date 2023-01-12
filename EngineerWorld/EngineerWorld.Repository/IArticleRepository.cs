using EngineerWorld.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Repository
{
    public interface IArticleRepository
    {
        public Task<Article> UpsertAsync(ArticleCreate articleCreate, int applicationUserId);

        public Task<ArticlePagedResults<Article>> GetAllAsync(ArticlePaging articlePaging);

        public Task<Article> GetAsync(int articleId);

        public Task<List<Article>> GetAllByUserIdAsync(int applicationUserId);

        public Task<List<Article>> GetAllFamousAsync();

        public Task<int> DeleteAsync(int articleId);
    }
}
