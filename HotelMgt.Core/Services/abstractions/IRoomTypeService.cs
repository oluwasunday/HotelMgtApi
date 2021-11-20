using HotelMgt.Dtos;
using HotelMgt.Dtos.RoomTypeDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IRoomTypeService
    {
        Task<Response<RoomTypeResponseDto>> AddRoomType(AddRoomTypeDto roomTypeDto);
        Task<Response<string>> DeleteRoomType(string roomTypeId);
        Task<Response<RoomTypeResponseDto>> GetRoomType(string roomTypeId);
        Response<List<RoomTypeResponseDto>> GetAllRoomTypes();
        Task<Response<string>> UpdateRoomType(string roomTypeId, UpdateRoomTypeDto roomTypeDto);
        Task<Response<RoomTypeResponseDto>> GetRoomTypeByName(string roomTypeName);
    }
}