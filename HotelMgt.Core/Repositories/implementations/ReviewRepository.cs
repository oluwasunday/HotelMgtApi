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
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly HotelMgtDbContext _context;
        public ReviewRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Review> GetReviewsByCustomerId(string customerId)
        {
            List<Review> reviews = _context.Reviews
                .Where(x => x.CustomerId == customerId)
                .ToList();

            return reviews;
        }
    }
}
