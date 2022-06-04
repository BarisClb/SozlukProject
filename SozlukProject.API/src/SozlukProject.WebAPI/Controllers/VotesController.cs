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
        public VotesController(VoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EntityListSortValues sortValues)
        {
            return Ok();
        }

        [HttpGet("{voteId}")]
        public async Task<IActionResult> Get(int voteId)
        {
            return Ok();
        }

        [HttpGet("ByComment/{commentId}")]
        public async Task<IActionResult> ByComment(int commentId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok();
        }

        [HttpGet("ByDiscussion/{discussionId}")]
        public async Task<IActionResult> ByDiscussion(int discussionId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok();
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> ByUser(int userId, [FromQuery] EntityListSortValues sortValues)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(VoteCreateDto voteCreateDto)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VoteUpdateDto voteUpdateDto)
        {
            return Ok();
        }

        [HttpDelete("{voteId}")]
        public async Task<IActionResult> Delete(int voteId)
        {
            return Ok();
        }
    }
}
