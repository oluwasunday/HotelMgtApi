using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.AuthenticationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
    }
}
