using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozlukProject.Service.Contracts;
using SozlukProject.Service.Dtos.Shared;
using SozlukProject.Service.Services.Implementation;

namespace SozlukProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly IJwtService _jwtService;

        public AccountsController(AccountService accountService, IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(AccountLoginInfo accountLoginInfo)
        {
            return Ok("Not implemented.");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            string? jwt = Request.Cookies[$"jwtUser"];
            if (jwt == null)
                return Ok(new { success = false, message = "Cookie does not exist." });

            Response.Cookies.Delete($"jwtUser", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            });


            return Ok(new { success = true, message = $"User logout successful." });
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> Verify()
        {
            string? jwt = Request.Cookies[$"jwtUser"];


            return Ok(_accountService.Verify(_jwtService.Verify(jwt)));
        }

        [HttpPost("SendActivationCode")]
        public async Task<IActionResult> SendActivationCode()
        {
            string? jwt = Request.Cookies[$"jwtUser"];
            await _accountService.SendActivationCode(jwt);


            return Ok(new { success = true, message = $"Activation code sent." });
        }

        [HttpPost("ActivateAccount")]
        public async Task<IActionResult> ActivateAccount(int activationCode)
        {
            string? jwt = Request.Cookies[$"jwtUser"];
            

            return Ok(await _accountService.ActivateAccount(jwt, activationCode));
        }
    }
}
