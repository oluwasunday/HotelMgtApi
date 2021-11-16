using HotelMgt.Core.interfaces;
using HotelMgt.Models;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.interfaces
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        Task<RoomType> GetRoomTypeByIdAsync(string roomTypeId);
        void UpdateRoomType(RoomType roomType);
    }
}