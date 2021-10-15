using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;

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
    }
}
