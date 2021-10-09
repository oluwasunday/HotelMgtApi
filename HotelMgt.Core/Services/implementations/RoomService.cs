using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.RoomDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hotel_booking_core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<RoomDto>> GetRooomById(string roomId)
        {
            var room = await _unitOfWork.Rooms.GetAsync(roomId);

            if (room != null)
            {
                var response = _mapper.Map<RoomDto>(room);

                var result = new Response<RoomDto>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Succeeded = true,
                    Message = $"successful",
                    Data = response
                };
                return result;
            }
            return Response<RoomDto>.Fail("Not Found");
        }

        public Response<List<RoomDto>> GetRoooms()
        {
            var room = _unitOfWork.Rooms.GetAll();

            if (room != null)
            {
                var response = _mapper.Map<List<RoomDto>>(room);

                var result = new Response<List<RoomDto>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Succeeded = true,
                    Message = $"successful",
                    Data = response
                };
                return result;
            }
            return Response<List<RoomDto>>.Fail("Not Found");
        }

        public async Task<Response<AddRoomResponseDto>> AddRoom(AddRoomDto roomDto)
        {
            Room room = _mapper.Map<Room>(roomDto);

            if(room != null)
            {
                room.Id = Guid.NewGuid().ToString();
                room.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.Rooms.AddAsync(room);
                await _unitOfWork.CompleteAsync();

                var roomResponse = _mapper.Map<AddRoomResponseDto>(room);

                var response = new Response<AddRoomResponseDto>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Succeeded = true,
                    Data = roomResponse,
                    Message = $"Room with id {room.Id} added"
                };
                return response;
            }
            return Response<AddRoomResponseDto>.Fail("Not Found");
        }
    }
}
