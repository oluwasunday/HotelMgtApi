﻿using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.GalleryDtos;
using HotelMgt.Dtos.RoomDtos;
using HotelMgt.Dtos.RoomTypeDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    [DataContract(IsReference = true)]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IGalleryService _galleryService;
        private readonly IImageService _imageService;

        public RoomController(IRoomService roomService, IRoomTypeService roomTypeService, 
            IGalleryService galleryService, IImageService imageService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _galleryService = galleryService;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult AllRooms()
        {
            var rooms = _roomService.GetRoooms();
            return StatusCode(rooms.StatusCode, rooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> RoomById(string roomId)
        {
            var room = await _roomService.GetRooomById(roomId);
            return StatusCode(room.StatusCode, room);
        }

        [HttpGet("type/{roomTypeId}")]
        public async Task<IActionResult> RoomByRoomTypeId(string roomTypeId)
        {
            var room = await _roomService.GetRooomByRoomTypeIdAsync(roomTypeId);
            return StatusCode(room.StatusCode, room);
        }

        [HttpPost()]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddRoom(AddRoomDto roomDto)
        {
            var room = await _roomService.AddRoom(roomDto);
            return StatusCode(room.StatusCode, room);
        }


        // RoomType section

        [HttpPost("roomtype")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddRoomType(AddRoomTypeDto roomTypeDto)
        {
            var roomType = await _roomTypeService.AddRoomType(roomTypeDto);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpGet("roomtypes")]
        public IActionResult GetAllRoomTypes()
        {
            var roomType = _roomTypeService.GetAllRoomTypes();
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpGet("roomtype/{roomTypeId}")]
        public async Task<IActionResult> GetRoomType(string roomTypeId)
        {
            var roomType = await _roomTypeService.GetRoomType(roomTypeId);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpGet("roomtypes/{roomTypeName}")]
        public async Task<IActionResult> GetRoomTypeByName(string roomTypeName)
        {
            var roomType = await _roomTypeService.GetRoomTypeByName(roomTypeName);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpDelete("roomtypes/{roomTypeId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteRoomType(string roomTypeId)
        {
            var roomType = await _roomTypeService.DeleteRoomType(roomTypeId);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpPut("roomtypes/{roomTypeId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateRoomType(string roomTypeId, UpdateRoomTypeDto roomTypeDto)
        {
            var roomType = await _roomTypeService.UpdateRoomType(roomTypeId, roomTypeDto);
            return StatusCode(roomType.StatusCode, roomType);
        }


        [HttpPost("{roomId}/gallery")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddRoomGallery(string roomId, [FromForm]AddImageDto model)
        {
            var image = await _imageService.UploadImageAsync(model.ImageUrl);
            var galleryDto = new AddGalleryDto { RoomId = roomId, IsFeature = model.IsFeature, ImageUrl = image.Url.ToString() };
            var result = await _galleryService.AddImageToRoom(galleryDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{roomId}/gallery")]
        public IActionResult RoomGallery(string roomId)
        {
            var result = _galleryService.GetGalleriesForARoom(roomId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{roomId}/gallery/")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateRoomPhotoById(UpdateRoomPhotoDto updateRoomPhoto)
        {
            var result = await _galleryService.UpdateRoomPhotoAsync(updateRoomPhoto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("gallery/{galleryId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteRoomPhotoById(string galleryId)
        {
            var result = await _galleryService.DeleteRoomPhotoAsync(galleryId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{roomId}/checkout")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Checkout(string roomId)
        {
            var result = await _roomService.CheckoutRooomById(roomId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("roomno/{roomNo}/checkout")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CheckoutByRoomNo(string roomNo)
        {
            var result = await _roomService.CheckoutRooomByRoomNo(roomNo);
            return StatusCode(result.StatusCode, result);
        }
    }
}
