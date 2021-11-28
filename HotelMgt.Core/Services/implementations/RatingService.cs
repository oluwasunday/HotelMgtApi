using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<AddRatingResponseDto>> AddRatings(string userId, AddRatingsDto ratingsDto)
        {
            Customer customer = await _unitOfWork.Customers.GetAsync(userId);
            if (customer == null)
                return Response<AddRatingResponseDto>.Fail("Customer not found");

            var prevRate = _unitOfWork.Ratings.GetRatingByCustomerId(userId).FirstOrDefault();
            var responseRate = _mapper.Map<AddRatingResponseDto>(prevRate);
            if (prevRate != null)
            {
                prevRate.Ratings = ratingsDto.Ratings;
                prevRate.Comment = ratingsDto.Comment;

                _unitOfWork.Ratings.UpdateRatingAsync(prevRate);
                await _unitOfWork.CompleteAsync();
                return Response<AddRatingResponseDto>.Success("success", responseRate);
            }

            Rating rating = _mapper.Map<Rating>(ratingsDto);
            rating.CustomerId = userId;

            await _unitOfWork.Ratings.AddAsync(rating);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddRatingResponseDto>(rating);
            return Response<AddRatingResponseDto>.Success("success", response);
        }

        public async Task<Response<List<Rating>>> GetAllRatingsAsync()
        {
            var ratings = await _unitOfWork.Ratings.GetRatingsAsync();

            return Response<List<Rating>>.Success("success", ratings.ToList());
        }

        public async Task<Response<AddRatingResponseDto>> GetRatingById(string id)
        {
            if(id == null)
                return new Dtos.Response<AddRatingResponseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = "Failed"
                };

            var rating = await _unitOfWork.Ratings.GetAsync(id);
            if (rating == null)
                return new Response<AddRatingResponseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = "Failed, Customer not found"
                };

            var ratingRespone = _mapper.Map<AddRatingResponseDto>(rating);

            return new Response<AddRatingResponseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = ratingRespone,
                Message = "Success"
            };
        }

        public async Task<Response<List<AddRatingResponseDto>>> GetAllRatingsByCustomerId(string customerId)
        {
            var customer = await _unitOfWork.Customers.GetAsync(customerId);
            if (customer == null)
                return new Response<List<AddRatingResponseDto>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = default,
                    Message = "Failed, customer not found"
                };

            var rating = _unitOfWork.Ratings.GetRatingByCustomerId(customerId);
            var ratingRespone = _mapper.Map<List<AddRatingResponseDto>>(rating);

            return new Response<List<AddRatingResponseDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = ratingRespone,
                Message = "Success"
            };
        }

        public Response<double> GetAllRatingsAverage()
        {
            double rating = _unitOfWork.Ratings.GetRatingAverage();

            return new Response<double>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = Math.Round(rating, 1),
                Message = "Success"
            };
        }

        public async Task<Response<double>> GetRatingsAverageByCustomer(string customerId)
        {
            var customer = await _unitOfWork.Customers.GetAsync(customerId);
            if(customer == null)
                return new Response<double>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = default,
                    Message = "Failed, customer not found"
                };

            double rating = _unitOfWork.Ratings.GetRatingAverageByCustomer(customerId);
            return new Response<double>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = Math.Round(rating, 1),
                Message = "Success"
            };
        }

        public async Task<Response<AddRatingResponseDto>> UpdateRating(UpdateRatingDto ratingDto)
        {
            var rating = await _unitOfWork.Ratings.GetAsync(ratingDto.Id);
            if (rating == null)
                return new Response<AddRatingResponseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = $"No rating with the id {ratingDto.Id} found",
                    Errors = null
                };
            
            rating.Ratings = ratingDto.Ratings;
            rating.UpdatedAt = DateTime.UtcNow;

            Rating result = _unitOfWork.Ratings.UpdateRatingAsync(rating);
            await _unitOfWork.CompleteAsync();

            AddRatingResponseDto response = _mapper.Map<AddRatingResponseDto>(result);

            return new Response<AddRatingResponseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = response,
                Message = "Successful",
                Errors = null
            };
        }
    }
}
