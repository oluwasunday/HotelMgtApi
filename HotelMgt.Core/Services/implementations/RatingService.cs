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

        public Response<List<AddRatingResponseDto>> GetAllRatings()
        {
            var ratings = _unitOfWork.Ratings.GetAll().ToList();
            var ratingRespone = _mapper.Map<List<AddRatingResponseDto>>(ratings);

            return new Response<List<AddRatingResponseDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = ratingRespone,
                Message = "Success"
            };
        }

        public async Task<Response<AddRatingResponseDto>> GetRatingById(string id)
        {
            var rating = await _unitOfWork.Ratings.GetAsync(id);
            var ratingRespone = _mapper.Map<AddRatingResponseDto>(rating);

            return new Response<AddRatingResponseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = ratingRespone,
                Message = "Success"
            };
        }

        public Response<List<AddRatingResponseDto>> GetAllRatingsByCustomerId(string customerId)
        {
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
                Data =  Math.Round(rating,1),
                Message = "Success"
            };
        }

        public Response<double> GetRatingsAverageByCustomer(string customerId)
        {
            double rating = _unitOfWork.Ratings.GetRatingAverageByCustomer(customerId);

            return new Response<double>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = Math.Round(rating,1),
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
            Rating result = await _unitOfWork.Ratings.UpdateRatingAsync(rating);
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
