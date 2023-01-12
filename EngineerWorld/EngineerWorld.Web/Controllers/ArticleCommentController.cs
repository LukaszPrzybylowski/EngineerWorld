using EngineerWorld.Model.ArticleComment;
using EngineerWorld.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EngineerWorld.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCommentController : ControllerBase
    {
        private readonly IArticleCommentRepository _articleCommentRepository;

        public ArticleCommentController(IArticleCommentRepository articleCommentRepository)
        {
            _articleCommentRepository = articleCommentRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ArticleComment>> CreateArticleComment(ArticleCommentCreate articleCommentCreate)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var createdArticleComment = await _articleCommentRepository.UpsertAsync(articleCommentCreate, applicaitonUserId);

            return Ok(createdArticleComment);
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult<List<ArticleComment>>> GetAllArticleComments(int articleId)
        {
            var articleComments = await _articleCommentRepository.GetAllAsync(articleId);

            return Ok(articleComments);
        }

        [Authorize]
        [HttpDelete("{articleCommentId}")]
        public async Task<ActionResult<int>> DeleteArticleComment(int articleCommentId)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundArticleComment = await _articleCommentRepository.GetAsync(articleCommentId);

            if (foundArticleComment == null) return BadRequest("Comment does not exists.");

            if(foundArticleComment.ApplicationUserId == applicaitonUserId)
            {
                var affectedRows = await _articleCommentRepository.DeleteAsync(articleCommentId);

                return Ok(affectedRows);
            }
            else
            {
                return BadRequest("This comment was not created by the current user.");
            }
        }
    }
}
