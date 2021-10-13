using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;

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
