using HotelMgt.Dtos;
using HotelMgt.Dtos.GalleryDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IGalleryService
    {
        Task<Response<AddGalleryResponseDto>> AddImageToRoom(AddGalleryDto galleryDto);
        Task<Response<string>> DeleteRoomPhotoAsync(string galleryId);
        Response<IEnumerable<AddGalleryResponseDto>> GetGalleriesForARoom(string roomId);
        Task<Response<string>> UpdateRoomPhotoAsync(UpdateRoomPhotoDto photoDto);
    }
}