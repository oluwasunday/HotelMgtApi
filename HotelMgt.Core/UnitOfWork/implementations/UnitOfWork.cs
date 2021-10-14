using HotelMgt.Core.interfaces;
using HotelMgt.Core.Repositories.implementations;
using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Data;
using System;
using System.Threading.Tasks;

namespace HotelMgt.Core.UnitOfWork.implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository Customers { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public IBookingsRepository Bookings { get; private set; }
        public IRatingRepository Ratings { get; private set; }
        public IReviewRepository Reviews { get; private set; }
        private readonly HotelMgtDbContext _context;

        public UnitOfWork(HotelMgtDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Bookings = new BookingsRepository(_context);
            Rooms = new RoomRepository(_context);
            Ratings = new RatingRepository(_context);
            Reviews = new ReviewRepository(_context);
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
