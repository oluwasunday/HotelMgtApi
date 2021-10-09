using HotelMgt.Dtos;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface ICustomerService
    {
        Task<Response<AddCustomerResponseDto>> AddCustomer(AddCustomerDto customerDto);
        Task<Response<AddCustomerResponseDto>> GetCustomerById(string id);
        IEnumerable<AddCustomerResponseDto> GetCustomers();
    }
}