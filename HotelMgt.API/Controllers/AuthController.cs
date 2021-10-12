using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.AuthenticationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMailService _mailService;
        private IConfiguration _configuration;
        public AuthController(IAuthenticationService authenticationService, IMailService mailService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _mailService = mailService;
            _configuration = configuration;
        }

        // base-url/Auth/Login
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto model)
        {
            var result = await _authenticationService.LoginUserAsync(model);
            return StatusCode(result.StatusCode, result);
        }


        // base-url/Auth/Register
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        //[Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterDto model)
        {
            var result = await _authenticationService.RegisterUserAsync(model);
            return StatusCode(result.StatusCode, result);
        }

        // base-url/Auth/sendmail
        [HttpPost]
        [Route("send-mail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendMail([FromForm] MailRequestDto model)
        {
            try
            {
                var result = await _mailService.SendEmailAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new[] { ex.Message, ex.StackTrace });
            }
            
        }

        // base-url/Auth/confirmemail
        [HttpGet]
        [Route("confirmemail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                var confirmEmail = new ConfirmEmailDto { Token = token, Email = email };
                var result = await _authenticationService.ConfirmEmailAsync(confirmEmail);
                return Redirect($"{_configuration["AppUrl"]}confirmemail.html");
            }
            catch (Exception ex)
            {
                return BadRequest(new[] { ex.Message, ex.StackTrace });
            }
        }

        // base-url/Auth/forget-password
        [HttpPost]
        [Route("forget-password")]
        [AllowAnonymous]
        //[Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await _authenticationService.ForgetPasswordAsync(email);
            return StatusCode(result.StatusCode, result);
        }

        // base-url/Auth/reset-password
        [HttpPost]
        [Route("resetpassword")]
        [AllowAnonymous]
        //[Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromForm]ResetPasswordDto model)
        {
            var result = await _authenticationService.ResetPasswordAsync(model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
