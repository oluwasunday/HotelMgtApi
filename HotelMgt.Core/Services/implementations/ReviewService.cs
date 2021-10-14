using AutoMapper;
using HotelMgt.Core.Repositories.interfaces;
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
    public class ReviewService
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
            if(customer == null)
                return new Response<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = "Customer not found"
                };
            Review review = _mapper.Map<Review>(reviewDto);
            await _unitOfWork.Reviews.AddAsync(review);

            return default;
        }
    }
}
