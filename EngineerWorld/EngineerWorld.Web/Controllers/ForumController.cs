using EngineerWorld.Model.Forum;
using EngineerWorld.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EngineerWorld.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumRepository _forumRepository;
        private readonly IPhotoRepository _photoRepository;

        public ForumController(IForumRepository forumRepository, IPhotoRepository photoRepository)
        {
            _forumRepository = forumRepository;
            _photoRepository = photoRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Forum>> CreateForum(ForumCreate forumCreate)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            if (forumCreate.PhotoId.HasValue)
            { 
                var photo = await _photoRepository.GetAsync(forumCreate.PhotoId.Value);

                if (photo.ApplicationUserId== applicaitonUserId)
                {
                    return BadRequest("You did not upload the photo.");
                }
            
            }

            var forum = await _forumRepository.UpsertAsync(forumCreate, applicaitonUserId);

            return Ok(forum);
        }

        [HttpGet]
        public async Task<ActionResult<ForumPagedResults<Forum>>> GetAllForum([FromQuery] ForumPaging forumPaging)
        {
            var forum = await _forumRepository.GetAllAsync(forumPaging);

            return Ok(forum);
        }

       
        [HttpGet("{forumId}")]
        public async Task<ActionResult<Forum>> GetForum(int forumId)
        {
            var forum = await _forumRepository.GetAsync(forumId);

            return Ok(forum);
        }

        [HttpGet("user/{applicationUserId}")]
        public async Task<ActionResult<List<Forum>>> GetForumByApplicationUserId(int applicationUserId)
        {
            var forums = await _forumRepository.GetAllByUserIdAsync(applicationUserId);

            return Ok(forums);
        }

        
        [HttpGet("famous")]
        public async Task<ActionResult<List<Forum>>> GetAllFamousForums()
        {
            var forums = await _forumRepository.GetAllFamousAsync();

            return Ok(forums);
        }

        [Authorize]
        [HttpDelete("{forumId}")]
        public async Task<ActionResult<int>> DeleteForum(int forumId)
        {
            int applicaitonUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundForum = await _forumRepository.GetAsync(forumId);

            if (foundForum == null) return BadRequest("Forum does not exist.");

            if(foundForum.ApplicationUserId == applicaitonUserId)
            {
                var affectedRows = await _forumRepository.DeleteAsync(foundForum.ApplicationUserId);

                return Ok(affectedRows);
            }
            else
            {
                return BadRequest("You didn't create this forum.");
            }
        }
    }


}
