using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Room> GetRoomAsync(string roomId)
        {
            var room = await _context.Rooms.Where(x => x.Id == roomId)
                .Include(x => x.Roomtype)
                .FirstOrDefaultAsync();
            return room;
        }

        public void UpdateBookedRoom(Room room)
        {
            _context.Rooms.Update(room);
        }

        public async Task<Room> GetRoomByRoomNoAsync(string roomNo)
        {
            var room = await _context.Rooms.Where(x => x.RoomNo == roomNo)
                .Include(x => x.Roomtype)
                .FirstOrDefaultAsync();
            return room;
        }
    }
}
