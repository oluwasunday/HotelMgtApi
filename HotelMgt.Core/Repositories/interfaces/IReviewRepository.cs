using HotelMgt.Core.interfaces;
using HotelMgt.Models;
using System.Collections.Generic;

namespace HotelMgt.Core.Repositories.interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        List<Review> GetReviewsByCustomerId(string customerId);
        Review UpdateReview(Review review);
    }
}