using HotelMgt.Core.interfaces;
using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Data;
using HotelMgt.Dtos.AmenityDtos;
using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.implementations
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly HotelMgtDbContext _context;
        public AmenityRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateAmenity(Amenity amenity)
        {
            _context.Amenities.Update(amenity);
        }
    }
}
