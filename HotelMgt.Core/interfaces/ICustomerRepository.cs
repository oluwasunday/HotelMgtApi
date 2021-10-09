using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Booking> GetTopCustomers(int count);

    }
}
