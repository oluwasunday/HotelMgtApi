using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        

        public async Task<Dtos.Response<AddBookingDto>> GetBookingAsync(string id)
        {
            var message = "";
            var booking = await _unitOfWork.Bookings.GetAsync(id);
            if(booking == null)
                message = "booking not available";
            
            var response = _mapper.Map<AddBookingDto>(booking);
            return new Dtos.Response<AddBookingDto>
            {
                StatusCode = message == "" ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                Succeeded = message == "" ? true : false,
                Data = response,
                Message = message == "" ? $"Booking with the id {booking.Id} added": message
            };
        }

        public Dtos.Response<List<AddBookingDto>> GetAllBookingAsync()
        {
            var message = "";
            var bookings = _unitOfWork.Bookings.GetAll().ToList();
            if (bookings == null)
                message = "No bookings available";

            var response = _mapper.Map<List<AddBookingDto>>(bookings);

            return new Dtos.Response<List<AddBookingDto>>
            {
                StatusCode = message == "" ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                Succeeded = message == "" ? true : false,
                Data = response,
                Message = message == "" ? "Success" : message
            };
        }

        public async Task<Dtos.Response<AddBookingResponseDto>> AddBookingAsync(string userId, AddBookingDto bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);
            booking.Id = Guid.NewGuid().ToString();
            booking.CustomerId = userId;

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.CompleteAsync();
            
            var responseDto = _mapper.Map<AddBookingResponseDto>(booking);

            return new Dtos.Response<AddBookingResponseDto>
            {
                StatusCode = StatusCodes.Status201Created,
                Succeeded = true,
                Data = responseDto,
                Message = "Successful!"
            };
        }
    }
}
