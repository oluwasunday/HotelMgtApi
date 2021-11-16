using HotelMgt.Dtos;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.PagingDtos;
using HotelMgt.Utilities.Paging;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IBookingService
    {
        Task<Response<AddBookingDto>> GetBookingAsync(string id);
        Response<PagedList<AddBookingDto>> GetAllBookingAsync(PagingDto paging);
        Task<Response<PagedList<BookingResponseDto>>> GetCustomerBookings(string userId, PagingDto paging);
        Task<Response<string>> VerifyBookingAsync(string trxRef);
        Task<Response<AddBookingResponseDto>> MakeBookingAsync(string userId, AddBookingDto bookingDto);
    }
}
