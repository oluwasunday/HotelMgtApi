using HotelMgt.Dtos;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IRatingService
    {
        Response<List<AddRatingResponseDto>> GetAllRatings();
        Response<double> GetAllRatingsAverage();
        Task<Response<List<AddRatingResponseDto>>> GetAllRatingsByCustomerId(string customerId);
        Task<Response<AddRatingResponseDto>> GetRatingById(string id);
        Task<Response<double>> GetRatingsAverageByCustomer(string customerId);
        Task<Response<AddRatingResponseDto>> UpdateRating(UpdateRatingDto ratingDto);
        Task<Response<AddRatingResponseDto>> AddRatings(AddRatingsDto ratingsDto);
    }
}