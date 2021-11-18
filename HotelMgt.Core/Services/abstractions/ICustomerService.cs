using HotelMgt.Dtos;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.ImageDtos;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface ICustomerService
    {
        Task<Response<AddCustomerResponseDto>> AddCustomerAsync(string userId, AddCustomerDto customerDto);
        Task<Response<AddCustomerResponseDto>> GetCustomerByIdAsync(string id);
        IEnumerable<AddCustomerResponseDto> GetCustomers();
        Task<Response<string>> UpdatePhotoAsync(string userId, AddImageDto imageDto);
    }
}