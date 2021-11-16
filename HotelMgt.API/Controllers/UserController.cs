using HotelMgt.Core.interfaces;
using HotelMgt.Dtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet()]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> Users()
        {
            try
            {
                return Ok(await _userRepository.GetAllUsersAsync());
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("add-user")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> AddUser(AddUserDto user)
        {
            try
            {
                return Ok(await _userRepository.AddUserAsync(user));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



        [HttpGet("{userId}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> UserById(string userId)
        {
            try
            {
                var result = await _userRepository.GetUserByIdAsync(userId);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("email")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> UserByEmail(string email)
        {
            try
            {
                return Ok(await _userRepository.GetUserByEmailAsync(email));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("searchTerm")]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult UserByTerm(string searchTerm)
        {
            try
            {
                return Ok(_userRepository.SearchUsersByTerm(searchTerm));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



        [HttpPut("{userId}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> UpdateUserById(string id, [FromBody]UpdateUserDto appUser)
        {
            try
            {
                return Ok(await _userRepository.UpdateUserAsync(id, appUser));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{userId}")]
        [Authorize]
        public async Task<ActionResult> UpdateUserPictureAsync(AppUser appUser)
        {
            try
            {
                return Ok(await _userRepository.UpdateUserPictureAsync(appUser));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
