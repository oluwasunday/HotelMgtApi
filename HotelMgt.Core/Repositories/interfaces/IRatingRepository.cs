using HotelMgt.Core.interfaces;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.interfaces
{
    public interface IRatingRepository : IRepository<Rating>
    {
        double GetRatingAverage();
        double GetRatingAverageByCustomer(string customerId);
        Rating UpdateRatingAsync(Rating rating);
        IEnumerable<Rating> GetRatingByCustomerId(string customerId);
        Task<Rating> UpdateRatingByCustomerIdAsync(string customerId, string ratingId, int ratingValue);
        Task<IEnumerable<Rating>> GetRatingsAsync();
    }
}