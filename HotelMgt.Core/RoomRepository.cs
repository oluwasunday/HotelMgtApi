using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly HotelMgtDbContext _context;
        public RoomRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
