using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.PagingDtos;
using HotelMgt.Models;
using HotelMgt.Utilities.helper;
using HotelMgt.Utilities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<AppUser> _userManager;
        private readonly string _baseUrl = "";

        public BookingController(
            IBookingService bookingService, 
            UserManager<AppUser> userManager, 
            IWebHostEnvironment web, IConfiguration config)
        {
            _bookingService = bookingService;
            _userManager = userManager;
            _baseUrl = web.IsDevelopment() ? config["MvcBaseUrl"] : config["MvcHerokuUrl"];
        }


        [HttpGet(Name = "GetAllBookings")]
        [Authorize(Roles = "Manager, Admin")]
        public IActionResult AllBookings([FromQuery]PagingDto paging)
        {
            var bookings = _bookingService.GetAllBookingAsync(paging);
            return Ok(bookings);
        }

        [HttpGet("customer-bookings")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> CustomerBookings(string customerId, [FromQuery] PagingDto paging)
        {
            var bookings = await _bookingService.GetCustomerBookings(customerId, paging);
            return StatusCode(bookings.StatusCode, bookings);
        }

        [HttpPost("create-booking")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddBooking([FromBody] AddBookingDto bookingDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var bookings = await _bookingService.MakeBookingAsync(user.Id, bookingDto);
            return StatusCode(bookings.StatusCode, bookings);
        }

        [HttpGet("verify-booking")]
        public async Task<IActionResult> VerifyBooking(string trxref, string reference)
        {
            var result = await _bookingService.VerifyBookingAsync(reference);
            return Redirect($"{_baseUrl}/Accomodation/SuccessBooking/");
            //return StatusCode(result.StatusCode, result);
        }


        // for advanced pagination
        private string CreateResourceUri(PagingDto pagedList, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAllBookings",
                        new {
                            pageNumber = pagedList.PageNumber - 1,
                            pageSize = pagedList.PageSize
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAllBookings",
                        new
                        {
                            pageNumber = pagedList.PageNumber + 1,
                            pageSize = pagedList.PageSize
                        });
                default:
                    return Url.Link("GetAllBookings",
                        new
                        {
                            pageNumber = pagedList.PageNumber,
                            pageSize = pagedList.PageSize
                        });
            }
        }
    }
}
