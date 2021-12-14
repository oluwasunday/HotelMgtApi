using HotelMgt.Dtos;
using HotelMgt.Dtos.RoomDtos;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IRoomService
    {
        Task<Response<RoomDto>> GetRooomById(string roomId);
        Task<Response<AddRoomResponseDto>> AddRoom(AddRoomDto roomDto);
        Response<List<RoomDto>> GetRoooms();
        Response<Room> GetRoomByNo(string roomNo);
        Task<Response<RoomDto>> CheckoutRooomById(string roomId);
        Task<Response<IEnumerable<RoomDto>>> GetRooomByRoomTypeIdAsync(string roomTypeId);
        Task<Response<RoomDto>> CheckoutRooomByRoomNo(string roomNo);
    }
}