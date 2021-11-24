using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.AmenityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/amenities")]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;
        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpPost()]
        [Authorize(Roles ="Manager, Admin")]
        public async Task<IActionResult> AddAmenity(AddAmenityDto amenityDto)
        {
            if (amenityDto != null)
            {
                var result = await _amenityService.AddAmenity(amenityDto);
                return StatusCode(result.StatusCode, result);
            }
            return BadRequest(amenityDto);
        }

        [HttpGet("id")]
        [Authorize(Roles ="Manager, Admin")]
        public async Task<IActionResult> GetAmenity(string id)
        {
            if (id != null)
            {
                var result = await _amenityService.GetAmenity(id);
                return StatusCode(result.StatusCode, result);
            }
            return BadRequest(id);
        }

        [HttpGet("roomtypeid/{roomTypeId}")]
        public async Task<IActionResult> GetAmenitiesByRoomTypeId(string roomTypeId)
        {
            if (roomTypeId != null)
            {
                var result = await _amenityService.GetAmenitiesByRoomTypeIdAsync(roomTypeId);
                return StatusCode(result.StatusCode, result);
            }
            return BadRequest(roomTypeId);
        }

        [HttpGet]
        public IActionResult Amenities()
        {
            try
            {
                var result = _amenityService.GetAmenities();
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPut("{id}/update")]
        [Authorize(Roles ="Manager, Admin")]
        public async Task<IActionResult> Amenities(string id, [FromBody]UpdateAmenityDto amenityDto)
        {
            try
            {
                var result = await _amenityService.UpdateAmenity(id, amenityDto);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}/delete")]
        [Authorize(Roles ="Manager, Admin")]
        public async Task<IActionResult> DeleteAmenity(string id)
        {
            var result = await _amenityService.DeleteAmenity(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
