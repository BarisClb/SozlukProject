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
    public class UsersController : ControllerBase
    {
        readonly private UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _userService.GetSortedUserList(sortValues));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            return Ok(await _userService.GetEntityById(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCreateDto userCreateDto)
        {
            return Ok(await _userService.CreateUser(userCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserUpdateDto userUpdateDto)
        {
            return Ok(await _userService.UpdateUser(userUpdateDto));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            return Ok(await _userService.DeleteUser(userId));
        }
    }
}
