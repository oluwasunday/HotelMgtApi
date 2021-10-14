using AutoMapper;
using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.ReviewDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> AddReview(AddReviewDto reviewDto)
        {
            Customer customer = await _unitOfWork.Customers.GetAsync(reviewDto.CustomerId);
            if (customer == null)
                return new Response<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = "Customer not found"
                };
            Review review = _mapper.Map<Review>(reviewDto);
            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.CompleteAsync();

            return new Response<string>
            {
                StatusCode = StatusCodes.Status201Created,
                Succeeded = true,
                Data = review.Comment,
                Message = "Comment successfully added"
            };
        }

        public Response<List<AddReviewResponseDto>> GetAllReviews()
        {
            var ratings = _unitOfWork.Reviews.GetAll().ToList();
            var ratingRespone = _mapper.Map<List<AddReviewResponseDto>>(ratings);

            return new Response<List<AddReviewResponseDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = ratingRespone,
                Message = "Success"
            };
        }

        public async Task<Response<AddReviewResponseDto>> GetReviewById(string id)
        {
            if(id == null)
                return new Response<AddReviewResponseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = "Failed, Customer not found"
                };

            var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null)
                return new Response<AddReviewResponseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = "Failed, Customer not found"
                };

            var reviewResponse = _mapper.Map<AddReviewResponseDto>(review);

            return new Response<AddReviewResponseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = reviewResponse,
                Message = "Success"
            };
        }

        public async Task<Response<List<AddReviewResponseDto>>> GetAllReviewsByCustomerId(string customerId)
        {
            var customer = await _unitOfWork.Customers.GetAsync(customerId);
            if (customer == null)
                return new Response<List<AddReviewResponseDto>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = default,
                    Message = "Failed, customer not found"
                };

            var review = _unitOfWork.Reviews.GetReviewsByCustomerId(customerId);
            var reviewResponse = _mapper.Map<List<AddReviewResponseDto>>(review);

            return new Response<List<AddReviewResponseDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = reviewResponse,
                Message = "Success"
            };
        }

        public async Task<Response<string>> DeleteReview(string id)
        {
            var review = await _unitOfWork.Reviews.GetAsync(id);
            if(review == null)
                return new Response<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = $"Review with id {review.Id} not found"
                };

            _unitOfWork.Reviews.Remove(review);
            await _unitOfWork.CompleteAsync();

            return new Response<string>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = null,
                Message = $"Review with id { review.Id } successfully deleted"
            };
        }

        public async Task<Response<AddReviewResponseDto>> UpdateReviewAsync(UpdateReviewDto reviewDto)
        {
            var review = await _unitOfWork.Reviews.GetAsync(reviewDto.Id);
            if (review == null)
                return new Response<AddReviewResponseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = $"No rating with the id {reviewDto.Id} found"
                };

            review.Comment = reviewDto.Comment;
            review.UpdatedAt = DateTime.UtcNow;

            Review result = _unitOfWork.Reviews.UpdateReview(review);
            await _unitOfWork.CompleteAsync();

            AddReviewResponseDto response = _mapper.Map<AddReviewResponseDto>(result);

            return new Response<AddReviewResponseDto>
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
