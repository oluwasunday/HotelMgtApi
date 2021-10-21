using AutoMapper;
using hotel_booking_models;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.GalleryDtos;
using HotelMgt.Dtos.ImageDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class GalleryService : IGalleryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GalleryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<AddGalleryResponseDto>> AddImageToRoom(AddGalleryDto galleryDto)
        {
            var gallery = _mapper.Map<Gallery>(galleryDto);
            if (gallery == null)
                return Response<AddGalleryResponseDto>.Fail("Invalid data");

            await _unitOfWork.Galleries.AddAsync(gallery);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddGalleryResponseDto>(gallery);

            return Response<AddGalleryResponseDto>.Success("Successfully added", response, StatusCodes.Status200OK);
        }

        public Response<IEnumerable<AddGalleryResponseDto>> GetGalleriesForARoom(string roomId)
        {
            var galleries = _unitOfWork.Galleries.Find(room => room.RoomId == roomId);

            var response = _mapper.Map<IEnumerable<AddGalleryResponseDto>>(galleries);

            return Response<IEnumerable<AddGalleryResponseDto>>
                .Success("Successfully added", response, StatusCodes.Status200OK);
        }

        public async Task<Response<string>> UpdateRoomPhotoAsync(UpdateRoomPhotoDto photoDto)
        {
            var gallery = await _unitOfWork.Galleries.GetAsync(photoDto.GalleryId);

            if (gallery == null)
                return Response<string>.Fail("Not found");

            gallery.ImageUrl = photoDto.ImageUrl;
            gallery.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Galleries.UpdateGallery(gallery);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Successfully updated", gallery.ImageUrl, StatusCodes.Status200OK);
        }

        public async Task<Response<string>> DeleteRoomPhotoAsync(string galleryId)
        {
            var gallery = await _unitOfWork.Galleries.GetAsync(galleryId);

            if (gallery == null)
                return Response<string>.Fail("Not found");

            _unitOfWork.Galleries.Remove(gallery);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Successfully deleted", gallery.ImageUrl, StatusCodes.Status204NoContent);
        }
    }
}
