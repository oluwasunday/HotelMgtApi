using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.RoomDtos;
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

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
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
    }
}
