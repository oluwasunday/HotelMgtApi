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
        private readonly DbSet<Room> _dbSet;
        public RoomRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Room>();
        }

        public async Task<Room> GetRoomAsync(string roomId)
        {
            var room = await _context.Rooms.Where(x => x.Id == roomId)
                .Include(x => x.Galleries)
                .Include(x => x.Roomtype)
                .FirstOrDefaultAsync();
            return room;
        }

        public IQueryable<Room> GetAllRoom()
        {
            var room = _dbSet.AsNoTracking()
                .Where(x => x.IsBooked == false)
                .Include(x => x.Galleries)
                .Include(y => y.Roomtype);
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

        public async Task<IEnumerable<Room>> GetRoomByRoomTypeIdAsync(string roomTypeId)
        {
            var room = await _context.Rooms
                .Where(x => x.RoomTypeId == roomTypeId && x.IsBooked == false).OrderBy(y => y.RoomNo)
                .Include(x => x.Galleries)
                .Include(x => x.Roomtype)
                .ToListAsync();
            return room;
        }
    }
}
