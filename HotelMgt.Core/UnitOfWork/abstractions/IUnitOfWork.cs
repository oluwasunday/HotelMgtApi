using HotelMgt.Core.interfaces;
using System;
using System.Threading.Tasks;

namespace HotelMgt.Core.UnitOfWork.abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IRoomRepository Rooms { get; }
        IBookingsRepository Bookings { get; }
        Task CompleteAsync();
    }
}
