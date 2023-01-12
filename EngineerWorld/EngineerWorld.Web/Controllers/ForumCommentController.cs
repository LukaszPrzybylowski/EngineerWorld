using EngineerWorld.Model.ArticleComment;
using EngineerWorld.Model.ForumComment;
using EngineerWorld.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EngineerWorld.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumCommentController : ControllerBase
    {
        private readonly IForumCommentRepository _forumCommentRepository;

        public ForumCommentController(IForumCommentRepository forumCommentRepository)
        {
            _forumCommentRepository = forumCommentRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ForumComment>> CreateForumComment(ForumCommentCreate forumCommentCreate)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var createdForumComment = await _forumCommentRepository.UpsertAsync(forumCommentCreate, applicaitonUserId);

            return Ok(createdForumComment);
        }

        [HttpGet("{forumId}")]
        public async Task<ActionResult<List<ForumComment>>> GetAllForumComments(int forumId)
        {
            var forumComments = await _forumCommentRepository.GetAllAsync(forumId);

            return Ok(forumComments);
        }

        [Authorize]
        [HttpDelete("{forumCommentId}")]
        public async Task<ActionResult<int>> DeleteForumComment(int forumCommentId)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundForumComment = await _forumCommentRepository.GetAsync(forumCommentId);

            if (foundForumComment == null) return BadRequest("Comment does not exists.");

            if (foundForumComment.ApplicationUserId == applicaitonUserId)
            {
                var affectedRows = await _forumCommentRepository.DeleteAsync(forumCommentId);

                return Ok(affectedRows);
            }
            else
            {
                return BadRequest("This comment was not created by the current user.");
            }
        }
    }
}
