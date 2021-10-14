using HotelMgt.Dtos;
using HotelMgt.Dtos.AmenityDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IAmenityService
    {
        Task<Response<AddAmenityResponseDto>> AddAmenity(AddAmenityDto amenityDto);
        Task<Response<AddAmenityResponseDto>> GetAmenity(string id);
        Response<List<AddAmenityResponseDto>> GetAmenities();
        Task<Response<AddAmenityResponseDto>> UpdateAmenity(string id, UpdateAmenityDto amenityDto);
        Task<Response<string>> DeleteAmenity(string id);
    }
}