﻿using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.AmenityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;
        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpPost()]
        //[Authorize(Roles ="Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        //[Authorize(Roles ="Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAmenity(string id)
        {
            if (id != null)
            {
                var result = await _amenityService.GetAmenity(id);
                return StatusCode(result.StatusCode, result);
            }
            return BadRequest(id);
        }

        [HttpGet("")]
        //[Authorize(Roles ="Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        //[Authorize(Roles ="Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        //[Authorize(Roles ="Manager, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAmenity(string id)
        {
            var result = await _amenityService.DeleteAmenity(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
