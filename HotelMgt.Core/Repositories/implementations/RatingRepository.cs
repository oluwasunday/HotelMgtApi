using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.implementations
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        private readonly HotelMgtDbContext _context;
        public RatingRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public double GetRatingAverage()
        {
            double ratings = _context.Ratings
                .Select(x => x.Ratings)
                .Average();

            return ratings;
        }

        public double GetRatingAverageByCustomer(string customerId)
        {
            double ratings = _context.Ratings
                .Where(x => x.CustomerId == customerId)
                .Select(x => x.Ratings)
                .Average();

            return ratings;
        }

        public async Task<Rating> UpdateRatingAsync(Rating rating)
        {
            _context.Update(rating);
            await _context.SaveChangesAsync();

            return rating;
        }

        public IEnumerable<Rating> GetRatingByCustomerId(string customerId)
        {
            var ratings = _context.Ratings
                .Where(x => x.CustomerId == customerId)
                .ToList();

            return ratings;
        }

        public async Task<Rating> UpdateRatingByCustomerIdAsync(string customerId, string ratingId, int ratingValue)
        {
            var rating = _context.Ratings
                .Where(x => x.CustomerId == customerId)
                .FirstOrDefault(y => y.Id == ratingId);

            if (rating != null)
            {
                rating.Ratings = ratingValue;
                _context.Update(rating);
                await _context.SaveChangesAsync();
            }

            return rating;
        }
    }
}
