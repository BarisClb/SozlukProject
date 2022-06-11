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
    public class DiscussionsController : ControllerBase
    {
        private readonly DiscussionService _discussionService;

        public DiscussionsController(DiscussionService discussionService)
        {
            _discussionService = discussionService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _discussionService.GetSortedDiscussionList(sortValues));
        }

        [HttpGet("{discussionId}")]
        public async Task<IActionResult> Get(int discussionId)
        {
            return Ok(await _discussionService.GetEntityById(discussionId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiscussionCreateDto discussionCreateDto)
        {
            return Ok(await _discussionService.CreateDiscussion(discussionCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(DiscussionUpdateDto discussionUpdateDto)
        {
            return Ok(await _discussionService.UpdateDiscussion(discussionUpdateDto));
        }

        [HttpDelete("{discussionId}")]
        public async Task<IActionResult> Delete(int discussionId)
        {
            return Ok(await _discussionService.DeleteEntity(discussionId, "Discussion"));
        }
    }
}
