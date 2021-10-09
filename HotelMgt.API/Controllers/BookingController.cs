﻿using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/dk-hotel")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<AppUser> _userManager;

        public BookingController(IBookingService bookingService, UserManager<AppUser> userManager)
        {
            _bookingService = bookingService;
            _userManager = userManager;
        }


        [HttpGet("id")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AllBookings()
        {
            var bookings = _bookingService.GetAllBookingAsync();
            return Ok(bookings);
        }

        [HttpPost("create-booking")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBooking([FromBody]AddBookingDto bookingDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var bookings = await _bookingService.AddBookingAsync("2ccd5586-51f2-444c-aa63-e13012748dfa", bookingDto);
            return StatusCode(bookings.StatusCode, bookings);
        }
    }
}
