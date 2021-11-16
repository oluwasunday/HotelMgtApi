using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.implementations
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        private readonly HotelMgtDbContext _context;
        public RoomTypeRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateRoomType(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
        }

        public async Task<RoomType> GetRoomTypeByIdAsync(string roomTypeId)
        {
            var roomType = await _context.RoomTypes
                .Where(x => x.Id == roomTypeId)
                .Include(y => y.Rooms)
                    .ThenInclude(z => z.Galleries)
                .FirstOrDefaultAsync();
            return roomType;
        }
    }
}
