using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozlukProject.Service.Contracts;
using SozlukProject.Service.Dtos.Account;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Response;
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
        public async Task<IActionResult> Login(AccountLoginInfoDto accountLoginInfo)
        {
            LoginResponse<UserReadDto> loginResponse = await _accountService.Login(accountLoginInfo);

            // Check if Login is Successful
            if (!loginResponse.Success)
            {
                // If cookie with that name exist, delete it
                string? jwt = Request.Cookies[$"jwtUser"];
                if (jwt != null)
                    Response.Cookies.Delete($"jwtUser");


                return Ok(loginResponse);
            }

            // If it succeeds, add token as a Cookie, based on the Account Type
            // We need to add options if we want to Append the Cookie while working with LocalHost. If we don't, it doesn't hold the Cookie.
            // HttpOnly prevents Client side scripts from accessing the data. 
            Response.Cookies.Append($"jwtUser", loginResponse.Jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            });


            return Ok(loginResponse);
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


            return Ok(await _accountService.Verify(jwt));
        }

        [HttpPost("SendActivationCode")]
        public async Task<IActionResult> SenActivationCode()
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

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(AccountChangePasswordDto accountChangePasswordDto)
        {
            return Ok(await _accountService.ChangePassword(accountChangePasswordDto));
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            return Ok(await _accountService.ForgotPassword(email));
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(AccountResetPasswordDto accountResetPasswordDto)
        {
            return Ok(await _accountService.ResetPassword(accountResetPasswordDto));
        }
    }
}
