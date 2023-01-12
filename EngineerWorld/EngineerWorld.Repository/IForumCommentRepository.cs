using EngineerWorld.Model.ForumComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorld.Repository
{
    public interface IForumCommentRepository
    {
        Task<ForumComment> UpsertAsync(ForumCommentCreate forumCommentCreate, int applicationUserId);

        Task<ForumComment> GetAsync(int forumCommentId);

        Task<List<ForumComment>> GetAllAsync(int forumId);

        Task<int> DeleteAsync(int forumCommentId);
    }
}
