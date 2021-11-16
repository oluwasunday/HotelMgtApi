using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room> GetRoomAsync(string roomId);
        Task<Room> GetRoomByRoomNoAsync(string roomNo);
        void UpdateBookedRoom(Room room);
    }
}
