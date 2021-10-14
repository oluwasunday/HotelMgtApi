using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IRatingService _ratingService;

        public CustomerController(ICustomerService customerService, UserManager<AppUser> userManager,
            IRatingService ratingService)
        {
            _customerService = customerService;
            _userManager = userManager;
            _ratingService = ratingService;
        }

        [HttpGet("id")]
        [Authorize]
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
        [Authorize]
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

        [HttpGet("ratings")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AllRatings()
        {
            var result = _ratingService.GetAllRatings();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("ratings/id")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RatingsByRatingId(string ratingId)
        {
            var result = await _ratingService.GetRatingById(ratingId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}/ratings")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AllRatingsByACustomer(string customerId)
        {
            var result = await _ratingService.GetAllRatingsByCustomerId(customerId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("ratings/average")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AllRatingsAverage()
        {
            var result = _ratingService.GetAllRatingsAverage();
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("ratings/customer/average")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RatingsAverageByCustomer(string customerId)
        {
            var result = await _ratingService.GetRatingsAverageByCustomer(customerId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("ratings/{ratingId}")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRating(string ratingId, int rateValue)
        {
            UpdateRatingDto ratingDto = new UpdateRatingDto { Id = ratingId, Ratings = rateValue };
            var result = await _ratingService.UpdateRating(ratingDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("ratings")]
        //[Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRatings(AddRatingsDto ratingsDto)
        {
            var result = await _ratingService.AddRatings(ratingsDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
