using HotelMgt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgt.Core.interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        IQueryable<Room> GetAllRoom();
        Task<Room> GetRoomAsync(string roomId);
        Task<Room> GetRoomByRoomNoAsync(string roomNo);
        Task<IEnumerable<Room>> GetRoomByRoomTypeIdAsync(string roomTypeId);
        void UpdateBookedRoom(Room room);
    }
}
