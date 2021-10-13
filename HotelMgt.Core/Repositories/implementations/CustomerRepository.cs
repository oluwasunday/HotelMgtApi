using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly HotelMgtDbContext _context;
        public CustomerRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetTopCustomers(int count)
        {
            return _context.Bookings
                .OrderByDescending(a => a.Payment.Amount)
                .Take(count)
                .Include(b => b.Customer).ToList();
        }
    }
}
