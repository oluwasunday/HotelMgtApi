using HotelMgt.Dtos;
using HotelMgt.Dtos.BookingDtos;
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
        Response<List<AddBookingDto>> GetAllBookingAsync();
        Task<Response<AddBookingResponseDto>> AddBookingAsync(string userId, AddBookingDto bookingDto);
    }
}
