using EngineerWorld.Model.Article;
using EngineerWorld.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EngineerWorld.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IPhotoRepository _photoRespository;

        public ArticleController(IArticleRepository articleRepository, IPhotoRepository photoRespository)
        {
            _articleRepository = articleRepository;
            _photoRespository = photoRespository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Article>> CreateArticle(ArticleCreate articleCreate)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            if (articleCreate.PhotoId.HasValue)
            {
                var photo = await _photoRespository.GetAsync(articleCreate.PhotoId.Value);

                if (photo.ApplicationUserId != applicaitonUserId)
                {
                    return BadRequest("You did not upload the photo.");

                }

            }

            var article = await _articleRepository.UpsertAsync(articleCreate, applicaitonUserId);

            return Ok(article);

        }

        [HttpGet]
        public async Task<ActionResult<ArticlePagedResults<Article>>> GetAllArticle([FromQuery] ArticlePaging articlePaging)
        {
            var articles = await _articleRepository.GetAllAsync(articlePaging);

            return Ok(articles);
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult<Article>> GetArticle(int articleId)
        {
            var article = await _articleRepository.GetAsync(articleId);

            return Ok(article);
        }

        [HttpGet("user/{applicationUserId}")]
        public async Task<ActionResult<List<Article>>> GetArticleByApplicationUserId(int applicationUserId)
        {
            var articles = await _articleRepository.GetAllByUserIdAsync(applicationUserId);

            return Ok(articles);
        }

        [HttpGet("famousArticles")]
        public async Task<ActionResult<List<Article>>> GetAllFamousArticles()
        {
            var articles = await _articleRepository.GetAllFamousAsync();

            return Ok(articles);
        }

        [Authorize]
        [HttpDelete("{articleId}")]
        public async Task<ActionResult<int>> DeleteArticle(int articleId)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundArticle = await _articleRepository.GetAsync(articleId);

            if (foundArticle == null) return BadRequest("Article does not exist.");

            if (foundArticle.ApplicationUserId == applicaitonUserId)
            { 
                var affectedRows = await _articleRepository.DeleteAsync(articleId);

                return Ok(affectedRows);
            }
            else
            {
                return BadRequest("You didn't create this blog.");
            }
        }
    }
}
