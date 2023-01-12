using EngineerWorld.Model.Photo;
using EngineerWorld.Repository;
using EngineerWorld.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Runtime.CompilerServices;

namespace EngineerWorld.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IForumRepository _forumRepository;
        private readonly IPhotoService _photoService;
      
        public PhotoController(
            IPhotoRepository photoRepository, 
            IArticleRepository articleRepository, 
            IForumRepository forumRepository, 
            IPhotoService photoService)
        {
            _photoRepository = photoRepository;
            _articleRepository = articleRepository;
            _forumRepository = forumRepository;
            _photoService = photoService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Photo>> UploadPhoto(IFormFile file)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var uploadResult = await _photoService.AddPhotoAsync(file);

            if(uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);

            var photoCreate = new PhotoCreate()
            {
                PublicId = uploadResult.PublicId,
                ImageUrl = uploadResult.SecureUri.AbsoluteUri,
                Description = file.FileName
            };

            var photo = await _photoRepository.InsertAsync(photoCreate, applicaitonUserId);

            return Ok(photo);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Photo>>> GetByApplicationUserId()
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var photos = await _photoRepository.GetAllByUserIdAsync(applicaitonUserId);

            return Ok(photos);
        }

        [HttpGet("{photoId}")]
        public async Task<ActionResult<Photo>> Get(int photoId)
        {
            var photo = await _photoRepository.GetAsync(photoId);

            return Ok(photo);  
        }

        [Authorize]
        [HttpDelete("{photoId}")]
        public async Task<ActionResult<int>> Delete(int photoId)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundPhoto = await _photoRepository.GetAsync(photoId);

            if(foundPhoto != null)
            {
                if (foundPhoto.ApplicationUserId == applicaitonUserId)
                {
                    var articles = await _articleRepository.GetAllByUserIdAsync(applicaitonUserId);

                    var forums = await _forumRepository.GetAllByUserIdAsync(applicaitonUserId);

                    var usedInArticle = articles.Any(b => b.PhotoId == photoId);

                    var usedInForum = forums.Any(b => b.PhotoId == photoId);

                    if (usedInArticle || usedInForum) return BadRequest("Cannot remove photo as it is being used in publsihed article(s) or in forum.");

                    var deleteResult = await _photoService.DeletePhotoAsync(foundPhoto.PublicId);

                    if (deleteResult.Error != null) return BadRequest(deleteResult.Error.Message);

                    var affectRows = await _photoRepository.DeleteAsync(foundPhoto.PhotoId);

                    return Ok(affectRows);
                }
                else              
                {
                    return BadRequest("Photo was not upload by the current user.");
                }
            }

            return BadRequest("Photo does not exsit.");
        }
    }
}
