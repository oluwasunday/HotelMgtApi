using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.ContactUsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Contacts()
        {
            var result = _contactUsService.GetAllContacts();
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Contact(string contactId)
        {
            var result = await _contactUsService.DeleteContactAsync(contactId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Contact(AddContactUsDto model)
        {
            var result = await _contactUsService.AddContactUsAsync(model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
