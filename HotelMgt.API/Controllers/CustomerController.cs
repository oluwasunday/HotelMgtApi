using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.ImageDtos;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Dtos.ReviewDtos;
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
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRatingService _ratingService;
        private readonly IImageService _imageService;

        public CustomerController(ICustomerService customerService, UserManager<AppUser> userManager,
            IRatingService ratingService, IImageService imageService)
        {
            _customerService = customerService;
            _userManager = userManager;
            _ratingService = ratingService;
            _imageService = imageService;
        }

        [HttpGet("id")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Admin")]
        public IActionResult AllCustomers()
        {
            var result = _customerService.GetCustomers();
            return Ok(result);
        }

        [HttpPost()]
        [Authorize(Roles = "Customer, Manager, Admin")]
        public async Task<IActionResult> AddCustomer([FromBody]AddCustomerDto customerDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _customerService.AddCustomerAsync(user.Id, customerDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("ratings")]
        [Authorize(Roles = "Manager, Admin")]
        public IActionResult AllRatings()
        {
            var result = _ratingService.GetAllRatings();
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("ratings/id")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> RatingsByRatingId(string ratingId)
        {
            var result = await _ratingService.GetRatingById(ratingId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("{customerId}/ratings")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> AllRatingsByACustomer(string customerId)
        {
            var result = await _ratingService.GetAllRatingsByCustomerId(customerId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("ratings/average")]
        [Authorize(Roles = "Manager, Admin")]
        public IActionResult AllRatingsAverage()
        {
            var result = _ratingService.GetAllRatingsAverage();
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("ratings/customer/average")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> RatingsAverageByCustomer(string customerId)
        {
            var result = await _ratingService.GetRatingsAverageByCustomer(customerId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPut("ratings/{ratingId}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> UpdateRating(string ratingId, int rateValue)
        {
            UpdateRatingDto ratingDto = new UpdateRatingDto { Id = ratingId, Ratings = rateValue };
            var result = await _ratingService.UpdateRating(ratingDto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("ratings")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddRatings(AddRatingsDto ratingsDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _ratingService.AddRatings(user.Id, ratingsDto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPatch("{customerId}/updateimage")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerImage(string customerId, IFormFile formFile)
        {
            //AppUser user = await _userManager.GetUserAsync(User);
            var upload = await _imageService.UploadImageAsync(formFile);
            
            AddImageDto imageDto = new AddImageDto() { Avatar = upload.Url.ToString(), PublicId = upload.PublicId};
            var updateUser = await _customerService.UpdatePhotoAsync(customerId, imageDto);

            if (updateUser != null)
                return StatusCode(updateUser.StatusCode, updateUser);

            return BadRequest("Pls provide required data");
        }
    }
}
