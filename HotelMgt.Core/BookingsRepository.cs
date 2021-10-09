using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core
{
    public class BookingsRepository : Repository<Booking>, IBookingsRepository
    {
        private readonly HotelMgtDbContext _context;
        public BookingsRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
