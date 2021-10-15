using HotelMgt.Core.interfaces;
using HotelMgt.Models;

namespace HotelMgt.Core.Repositories.interfaces
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        void UpdateRoomType(RoomType roomType);
    }
}