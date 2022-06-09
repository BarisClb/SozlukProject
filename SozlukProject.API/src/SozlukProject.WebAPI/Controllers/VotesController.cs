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
    public class VotesController : ControllerBase
    {
        readonly private VoteService _voteService;
        readonly private DiscussionPageService _discussionPageService;

        public VotesController(VoteService voteService, DiscussionPageService discussionPageService)
        {
            _voteService = voteService;
            _discussionPageService = discussionPageService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _voteService.GetSortedVoteList(sortValues));
        }

        [HttpGet("{voteId}")]
        public async Task<IActionResult> Get(int voteId)
        {
            return Ok(await _voteService.GetEntityById(voteId));
        }

        [HttpGet("ByComment/{commentId}")]
        public async Task<IActionResult> ByComment(int commentId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _voteService.GetSortedVoteList(sortValues, "Comment", commentId));
        }

        [HttpGet("ByDiscussion/{discussionId}")]
        public async Task<IActionResult> ByDiscussion(int discussionId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _voteService.GetSortedVoteList(sortValues, "Discussion", discussionId));
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> ByUser(int userId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _voteService.GetSortedVoteList(sortValues, "User", userId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VoteCreateDto voteCreateDto)
        {
            return Ok(await _discussionPageService.CreateVote(voteCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(VoteUpdateDto voteUpdateDto)
        {
            return Ok(await _discussionPageService.UpdateVote(voteUpdateDto));
        }

        [HttpDelete("{voteId}")]
        public async Task<IActionResult> Delete(int voteId)
        {
            return Ok(await _voteService.DeleteVote());
        }
    }
}
