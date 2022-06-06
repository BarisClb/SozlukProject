using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Update;
using SozlukProject.Service.Services;

namespace SozlukProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        readonly private CommentService _commentService;
        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _commentService.GetSortedCommentList(sortValues));
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> Get(int commentId)
        {
            return Ok(await _commentService.GetEntityById(commentId));
        }

        [HttpGet("ByDiscussion/{discussionId}")]
        public async Task<IActionResult> ByDiscussion(int discussionId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _commentService.GetSortedCommentList(sortValues, "Discussion", discussionId));
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> ByUser(int userId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _commentService.GetSortedCommentList(sortValues, "User", userId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CommentCreateDto commentCreateDto)
        {
            return Ok(await _commentService.CreateComment(commentCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(CommentUpdateDto commentUpdateDto)
        {
            return Ok(await _commentService.UpdateComment(commentUpdateDto));
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            return Ok(await _commentService.DeleteEntity(commentId));
        }
    }
}
