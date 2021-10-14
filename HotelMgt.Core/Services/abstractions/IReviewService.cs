using HotelMgt.Dtos;
using HotelMgt.Dtos.ReviewDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IReviewService
    {
        Task<Response<string>> AddReview(AddReviewDto reviewDto);
        Response<List<AddReviewResponseDto>> GetAllReviews();
        Task<Response<AddReviewResponseDto>> GetReviewById(string id);
        Task<Response<List<AddReviewResponseDto>>> GetAllReviewsByCustomerId(string customerId);
        Task<Response<string>> DeleteReview(string id);
        Task<Response<AddReviewResponseDto>> UpdateReviewAsync(UpdateReviewDto reviewDto);
    }
}