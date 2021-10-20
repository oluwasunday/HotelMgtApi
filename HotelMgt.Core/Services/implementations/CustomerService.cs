using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.ImageDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<AddCustomerResponseDto>> GetCustomerById(string id)
        {
            var message = "";

            var customer = await _unitOfWork.Customers.GetAsync(id);
            
            if (customer == null)
                message = "Invalid user Id";
            
            var response = _mapper.Map<AddCustomerResponseDto>(customer);
            var result = new Response<AddCustomerResponseDto>()
            {
                StatusCode = message == ""? (int)StatusCodes.Status200OK : (int)StatusCodes.Status400BadRequest,
                Succeeded = message == ""? true: false,
                Data = response,
                Message = message == "" ? $"Customer with the user id {customer.AppUserId} added!" : message
            };
            
            return result;
        }

        public async Task<Response<AddCustomerResponseDto>> AddCustomer(AddCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var user = await _userManager.FindByIdAsync(customer.AppUserId);
            if(user == null) { return Response<AddCustomerResponseDto>.Fail("Account not found, customer needs to register"); };

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
            await _userManager.AddToRoleAsync(user, "Customer");

            var response = _mapper.Map<AddCustomerResponseDto>(customer);

            return new Response<AddCustomerResponseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = response,
                Message = $"Customer with the user id {customer.AppUserId} added!"
            };
        }

        public IEnumerable<AddCustomerResponseDto> GetCustomers()
        {
            var customers =  _unitOfWork.Customers.GetAll();
            var response =  _mapper.Map<List<AddCustomerResponseDto>>(customers);

            return response;
        }

        public async Task<Response<string>> UpdatePhotoAsync(string userId, AddImageDto imageDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Response<string>.Fail(nameof(userId) + " not found");
                

            user.PublicId = imageDto.PublicId;
            user.Avatar = imageDto.Avatar;
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return Response<string>.Success("Successfully updated", user.Avatar);
        }
    }
}
