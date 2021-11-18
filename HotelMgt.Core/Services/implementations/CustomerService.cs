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

        public async Task<Response<AddCustomerResponseDto>> GetCustomerByIdAsync(string id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(id);
            if (customer == null)
                return Response<AddCustomerResponseDto>.Fail($"Customer not found");

            var response = _mapper.Map<AddCustomerResponseDto>(customer);
            
            return Response<AddCustomerResponseDto>.Success($"success", response);
        }

        public async Task<Response<AddCustomerResponseDto>> AddCustomerAsync(string userId, AddCustomerDto customerDto)
        {
            Customer customer = _mapper.Map<Customer>(customerDto);
            customer.AppUserId = userId;
            customer.CreditCard = "default";

            AppUser user = await _userManager.FindByIdAsync(userId);
            if(user == null) { return Response<AddCustomerResponseDto>.Fail("Account not found, customer needs to register"); };

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
            await _userManager.AddToRoleAsync(user, "Customer");

            var response = _mapper.Map<AddCustomerResponseDto>(customer);

            return Response<AddCustomerResponseDto>.Success("success", response);
        }

        public IEnumerable<AddCustomerResponseDto> GetCustomers()
        {
            IEnumerable<Customer> customers =  _unitOfWork.Customers.GetAll();
            List<AddCustomerResponseDto> response =  _mapper.Map<List<AddCustomerResponseDto>>(customers);

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
