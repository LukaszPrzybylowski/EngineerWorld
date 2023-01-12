using EngineerWorld.Model.Article;
using EngineerWorld.Model.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Repository
{
    public interface IForumRepository
    {
        public Task<Forum> GetAsync(int forumId);

        public Task<ForumPagedResults<Forum>> GetAllAsync(ForumPaging forumPaging);

        public Task<List<Forum>> GetAllFamousAsync();

        public Task<List<Forum>> GetAllByUserIdAsync(int applicationUserId);

        public Task<int> DeleteAsync(int forumId);

        public Task<Forum> UpsertAsync(ForumCreate forumCreate, int applicationUserId);
    }
}
