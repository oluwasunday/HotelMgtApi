using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.RoomTypeDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<RoomTypeResponseDto>> AddRoomType(AddRoomTypeDto roomTypeDto)
        {
            RoomType roomType = _mapper.Map<RoomType>(roomTypeDto);

            await _unitOfWork.RoomTypes.AddAsync(roomType);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<RoomTypeResponseDto>(roomType);
            return new Response<RoomTypeResponseDto>()
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Message = "Added successfully",
                Data = response
            };
        
        }

        public async Task<Response<RoomTypeResponseDto>> GetRoomType(string roomTypeId)
        {
            RoomType roomType = await _unitOfWork.RoomTypes.GetAsync(roomTypeId);
            if (roomType == null)
                return Response<RoomTypeResponseDto>.Fail("Not found!");

            var response = _mapper.Map<RoomTypeResponseDto>(roomType);
            return Response<RoomTypeResponseDto>.Success(response);
        }

        public Response<List<RoomTypeResponseDto>> GetAllRoomTypes()
        {
            var roomTypes = _unitOfWork.RoomTypes.GetAll();
            if (roomTypes == null)
                return Response<List<RoomTypeResponseDto>>.Fail("Not content found!");

            var response = _mapper.Map<List<RoomTypeResponseDto>>(roomTypes);
            return Response<List<RoomTypeResponseDto>>.Success(response);
        }

        public async Task<Response<string>> DeleteRoomType(string roomTypeId)
        {
            RoomType roomType = await _unitOfWork.RoomTypes.GetAsync(roomTypeId);
            if (roomType == null)
                return Response<string>.Fail("Not found!");

            _unitOfWork.RoomTypes.Remove(roomType);
            await _unitOfWork.CompleteAsync();

            return new Response<string>()
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Message = "Deleted successfully",
                Data = null
            };
        }

        public async Task<Response<string>> UpdateRoomType(string roomTypeId, UpdateRoomTypeDto roomTypeDto)
        {
            RoomType roomType = await _unitOfWork.RoomTypes.GetAsync(roomTypeId);
            if (roomType == null)
                return Response<string>.Fail("Not found!");

            roomType.Name = roomTypeDto.Name;
            roomType.Description = roomTypeDto.Description;
            roomType.Discount = roomTypeDto.Discount;
            roomType.Price = roomTypeDto.Price;
            roomType.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.RoomTypes.UpdateRoomType(roomType);
            await _unitOfWork.CompleteAsync();

            return new Response<string>()
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Message = "Updated successfully",
                Data = null
            };
        }
    }
}
