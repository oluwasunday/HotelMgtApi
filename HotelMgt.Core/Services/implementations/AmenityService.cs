using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.AmenityDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AmenityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<AddAmenityResponseDto>> AddAmenity(AddAmenityDto amenityDto)
        {
            var amenity = _mapper.Map<Amenity>(amenityDto);

            await _unitOfWork.Amenities.AddAsync(amenity);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddAmenityResponseDto>(amenity);

            return new Response<AddAmenityResponseDto>()
            {
                StatusCode = StatusCodes.Status201Created,
                Succeeded = true,
                Data = response,
                Message = $"Amenity with id {response.Id} successfully added"
            };
        }

        public async Task<Response<AddAmenityResponseDto>> GetAmenity(string id)
        {
            var amenity = await _unitOfWork.Amenities.GetAsync(id);
            if(amenity == null)
                return new Response<AddAmenityResponseDto>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = $"Failed, no amenity with the id {id} found"
                };

            var response = _mapper.Map<AddAmenityResponseDto>(amenity);
            return new Response<AddAmenityResponseDto>()
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = response,
                Message = "Successful"
            };
        }

        public Response<List<AddAmenityResponseDto>> GetAmenities()
        {
            var amenity = _unitOfWork.Amenities.GetAll();

            var response = _mapper.Map<List<AddAmenityResponseDto>>(amenity);
            return new Response<List<AddAmenityResponseDto>>()
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = response,
                Message = "Successful"
            };
        }

        public async Task<Response<AddAmenityResponseDto>> UpdateAmenity(string id, UpdateAmenityDto amenityDto)
        {
            var amenity = await _unitOfWork.Amenities.GetAsync(id);
            if(amenity == null)
                return new Response<AddAmenityResponseDto>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = $"Failed, no amenity with the id {id} found"
                };

            amenity.UpdatedAt = DateTime.UtcNow;
            amenity.Name = amenityDto.Name;

            _unitOfWork.Amenities.UpdateAmenity(amenity);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddAmenityResponseDto>(amenity);
            return new Response<AddAmenityResponseDto>()
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = response,
                Message = "Successful"
            };
        }

        public async Task<Response<string>> DeleteAmenity(string id)
        {
            var amenity = await _unitOfWork.Amenities.GetAsync(id);
            if (amenity == null)
                return new Response<string>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false,
                    Data = null,
                    Message = $"Failed, no amenity with the id {id} found"
                };

            _unitOfWork.Amenities.Remove(amenity);
            await _unitOfWork.CompleteAsync();

            return new Response<string>()
            {
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true,
                Data = null,
                Message = "Successfully deleted"
            };
        }


        public async Task<Response<IEnumerable<AddAmenityResponseDto>>> GetAmenitiesByRoomTypeIdAsync(string id)
        {
            var amenity = await _unitOfWork.Amenities.GetAmenityByRoomTypeIdAsync(id);
            if (amenity == null)
                return Response<IEnumerable<AddAmenityResponseDto>>.Fail($"Failed, no amenity with the id {id} found");

            var response = _mapper.Map<IEnumerable<AddAmenityResponseDto>>(amenity);
            return Response<IEnumerable<AddAmenityResponseDto>>.Success("success", response);
        }
    }
}
