using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelMgt.Core
{
    public class BookingsRepository : Repository<Booking>, IBookingsRepository
    {
        private readonly HotelMgtDbContext _context;
        public BookingsRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Booking> GetBookingsByCustomerId(string customerId)
        {
            var query = _context.Bookings.AsNoTracking()
                .Where(b => b.CustomerId == customerId)
                .Include(b => b.Payment)
                .Include(b => b.Room)
                .ThenInclude(r => r.Roomtype)
                .OrderByDescending(b => b.CreatedAt);
            return query;
        }

        public IQueryable<Booking> GetBookings()
        {
            var query = _context.Bookings.AsNoTracking()
                .Where(x => x.BookingStatus == "success" || x.BookingStatus == "Success")
                .Include(b => b.Customer)
                .ThenInclude(c => c.AppUser)
                .Include(b => b.Payment)
                .OrderByDescending(b => b.Payment.Amount);
            return query;
        }

        public bool UpdateBooking(Booking model)
        {
            var booking = _context.Bookings.Update(model);
            return true;
        }
    }
}
