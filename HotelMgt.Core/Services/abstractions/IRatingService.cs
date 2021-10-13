using HotelMgt.Dtos;
using HotelMgt.Dtos.RatingDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IRatingService
    {
        Response<List<AddRatingResponseDto>> GetAllRatings();
        Response<double> GetAllRatingsAverage();
        //Response<AddRatingResponseDto> GetAllRatingsByCustomerId(string customerId);
        Response<List<AddRatingResponseDto>> GetAllRatingsByCustomerId(string customerId);
        Task<Response<AddRatingResponseDto>> GetRatingById(string id);
        Response<double> GetRatingsAverageByCustomer(string customerId);
        Task<Response<AddRatingResponseDto>> UpdateRating(UpdateRatingDto ratingDto);
    }
}