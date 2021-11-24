using HotelMgt.Core.interfaces;
using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.interfaces
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        Task<IEnumerable<Amenity>> GetAmenityByRoomTypeIdAsync(string roomtypeId);
        void UpdateAmenity(Amenity amenity);
    }
}
