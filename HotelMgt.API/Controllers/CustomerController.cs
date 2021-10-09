using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;

        public CustomerController(ICustomerService customerService, UserManager<AppUser> userManager)
        {
            _customerService = customerService;
            _userManager = userManager;
        }

        [HttpGet("id")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var result = await _customerService.GetCustomerById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("customers")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AllCustomers()
        {
            var result = _customerService.GetCustomers();
            return Ok(result);
        }

        [HttpPost()]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomer([FromBody]AddCustomerDto customerDto)
        {
            var result = await _customerService.AddCustomer(customerDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
