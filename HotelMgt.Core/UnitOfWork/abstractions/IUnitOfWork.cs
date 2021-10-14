using HotelMgt.Core.interfaces;
using HotelMgt.Core.Repositories.interfaces;
using System;
using System.Threading.Tasks;

namespace HotelMgt.Core.UnitOfWork.abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IRoomRepository Rooms { get; }
        IBookingsRepository Bookings { get; }
        IRatingRepository Ratings { get; }
        IReviewRepository Reviews { get; }
        Task CompleteAsync();
    }
}
