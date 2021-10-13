using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.interfaces
{
    public interface IBookingsRepository : IRepository<Booking>
    {
    }
}
