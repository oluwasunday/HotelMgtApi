using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.PagingDtos;
using HotelMgt.Models;
using HotelMgt.Utilities;
using HotelMgt.Utilities.helper;
using HotelMgt.Utilities.Paging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PayStack.Net;
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
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IPaymentService _paymentService;

        private PayStackApi PayStack { get; set; }

        public BookingService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, IConfiguration configuration, 
            IWebHostEnvironment env,
            IPaymentService paymentService
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _env = env;
            _paymentService = paymentService;
            PayStack = new PayStackApi(_configuration["Payment:PaystackSecretKey"]);
        }
        

        public async Task<Response<AddBookingDto>> GetBookingAsync(string id)
        {
            var message = "";
            var booking = await _unitOfWork.Bookings.GetAsync(id);
            if(booking == null)
                message = "booking not available";
            
            var response = _mapper.Map<AddBookingDto>(booking);
            return new Response<AddBookingDto>
            {
                StatusCode = message == "" ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                Succeeded = message == "" ? true : false,
                Data = response,
                Message = message == "" ? $"Booking with the id {booking.Id} added": message
            };
        }

        public Response<PagedList<AddBookingDto>> GetAllBookingAsync(PagingDto paging)
        {
            var bookings = _unitOfWork.Bookings.GetBookings().ToList();
            if (bookings == null)
                return Response<PagedList<AddBookingDto>>.Fail("No bookings available");

            var response = _mapper.Map<List<AddBookingDto>>(bookings).ToList();
            var pageResult = PagedList<AddBookingDto>.Create(response, paging.PageNumber, paging.PageSize);

            return Response<PagedList<AddBookingDto>>.Success("success", pageResult);
        }

        public async Task<Response<AddBookingResponseDto>> MakeBookingAsync(string userId, AddBookingDto bookingDto)
        {
            var room = await _unitOfWork.Rooms.GetRoomByRoomNoAsync(bookingDto.RoomNo);
            if (room == null)
                return Response<AddBookingResponseDto>.Fail("Room not found");
            

            if (room.IsBooked)
                return Response<AddBookingResponseDto>.Fail("Room already booked", StatusCodes.Status422UnprocessableEntity);

            var customer = await _unitOfWork.Customers.GetCustomerAsync(userId);
            if (customer == null)
                return Response<AddBookingResponseDto>.Fail("Customer not found");

            string baseUrl = _env.IsProduction() ? _configuration["HerokuUrl"] : _configuration["BaseUrl"];

            var booking = _mapper.Map<Booking>(bookingDto);
            booking.RoomId = room.Id;
            booking.CustomerId = userId;
            booking.BookingReference = $"{_configuration["HotelName"]}-{room.Roomtype.Name}-{ReferenceGenerator.Generate()}";
            await _unitOfWork.Bookings.AddAsync(booking);

            int noOfDays =  booking.CheckOut.Day - booking.CheckIn.Day;

            var amount = (room.Roomtype.Price - (room.Roomtype.Price * room.Roomtype.Discount)) * noOfDays;
            var trxRef = $"TrxRef-{ReferenceGenerator.Generate()}";

            string authUrl = await _paymentService.MakePaymentAsync(amount, customer.AppUser.Email, booking.Id, trxRef);
            
            var bookingResponse = _mapper.Map<AddBookingResponseDto>(booking);
            bookingResponse.Price = amount;
            bookingResponse.AuthorizationUrl = authUrl;

            if (authUrl != "false")
            {
                room.IsBooked = true;
                _unitOfWork.Rooms.UpdateBookedRoom(room);
                await _unitOfWork.CompleteAsync();

                return Response<AddBookingResponseDto>.Success("Success", bookingResponse);
            }

            return Response<AddBookingResponseDto>.Fail($"Something went wrong {authUrl}");
        }

        public async Task<Response<PagedList<BookingResponseDto>>> GetCustomerBookings(string userId, PagingDto paging)
        {
            var customer = await _unitOfWork.Customers.GetAsync(userId);
            if (customer == null)
                return Response<PagedList<BookingResponseDto>>.Fail("Customer not found");
            
            var bookings = _unitOfWork.Bookings.GetBookingsByCustomerId(userId);

            var bookingResponse = _mapper.Map<IEnumerable<BookingResponseDto>>(bookings).ToList();
            var pageResult = PagedList<BookingResponseDto>.Create(bookingResponse, paging.PageNumber, paging.PageSize);
            
            return Response<PagedList<BookingResponseDto>>.Success("success", pageResult);
        }

        public async Task<Response<string>> VerifyBookingAsync(string trxRef)
        {
            var payment = await _unitOfWork.Payments.GetPaymentByTransactionReferenceAsync(trxRef);
            if (payment == null)
                return Response<string>.Fail("Payment details not found");

            if (payment.Status == true)
            {
                //_logger.Error($"Payment with payment reference {bookingDto.TrxRef} has already been verified");
                return Response<string>.Fail("Payment already verified", StatusCodes.Status409Conflict);
            }

            var paymentVerified = await _paymentService.VerifyPaymentAsync(payment.TransactionReference);
            if (paymentVerified.Succeeded)
            {
                var booking = await _unitOfWork.Bookings.GetAsync(payment.BookingId);
                booking.BookingStatus = "Success";
                _unitOfWork.Bookings.UpdateBooking(booking);

                payment.Status = true;
                _unitOfWork.Payments.UpdatePayment(payment);
                await _unitOfWork.CompleteAsync();
                //_logger.Information($"Payment with payment reference {bookingDto.TransactionReference} verified");
                return Response<string>.Success("success", "Verified");
            }

            

            payment.Status = false;
            payment.Booking.Room.IsBooked = false;
            
            _unitOfWork.Rooms.UpdateBookedRoom(payment.Booking.Room);
            _unitOfWork.Payments.UpdatePayment(payment);

            await _unitOfWork.CompleteAsync();
            
            //_logger.Error($"Payment with payment reference {bookingDto.TransactionReference} not verified");
            return Response<string>.Fail("Booking not Verified", StatusCodes.Status402PaymentRequired);
        }
    }
}
