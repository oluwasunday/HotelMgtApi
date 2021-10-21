using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.GalleryDtos;
using HotelMgt.Dtos.RoomDtos;
using HotelMgt.Dtos.RoomTypeDtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/rooms")]
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
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AllRooms()
        {
            var rooms = _roomService.GetRoooms();
            return StatusCode(rooms.StatusCode, rooms);
        }

        [HttpGet("id")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RoomById(string roomId)
        {
            var room = await _roomService.GetRooomById(roomId);
            return StatusCode(room.StatusCode, room);
        }

        [HttpPost()]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRoom(AddRoomDto roomDto)
        {
            var room = await _roomService.AddRoom(roomDto);
            return StatusCode(room.StatusCode, room);
        }


        // RoomType section

        [HttpPost("roomtype")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRoomType(AddRoomTypeDto roomTypeDto)
        {
            var roomType = await _roomTypeService.AddRoomType(roomTypeDto);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpGet("roomtypes")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllRoomTypes()
        {
            var roomType = _roomTypeService.GetAllRoomTypes();
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpGet("roomtype/id")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoomType(string roomTypeId)
        {
            var roomType = await _roomTypeService.GetRoomType(roomTypeId);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpDelete("roomtype/id")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRoomType(string roomTypeId)
        {
            var roomType = await _roomTypeService.DeleteRoomType(roomTypeId);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpPut("roomtype/{roomTypeId}")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRoomType(string roomTypeId, UpdateRoomTypeDto roomTypeDto)
        {
            var roomType = await _roomTypeService.UpdateRoomType(roomTypeId, roomTypeDto);
            return StatusCode(roomType.StatusCode, roomType);
        }

        [HttpPost("rooms/gallery")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRoomGallery(AddGalleryDto galleryDto)
        {
            var result = await _galleryService.AddImageToRoom(galleryDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("rooms/{roomId}/gallery")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RoomGallery(string roomId)
        {
            var result = _galleryService.GetGalleriesForARoom(roomId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("rooms/{roomId}/gallery/")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRoomPhotoById(UpdateRoomPhotoDto updateRoomPhoto)
        {
            var result = await _galleryService.UpdateRoomPhotoAsync(updateRoomPhoto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("gallery/{galleryId}")]
        //[Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRoomPhotoById(string galleryId)
        {
            var result = await _galleryService.DeleteRoomPhotoAsync(galleryId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
