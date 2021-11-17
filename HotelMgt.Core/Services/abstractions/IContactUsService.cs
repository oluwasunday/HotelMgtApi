using HotelMgt.Dtos;
using HotelMgt.Dtos.ContactUsDtos;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IContactUsService
    {
        Task<Response<ContactUs>> AddContactUsAsync(AddContactUsDto contactUsDto);
        Task<Response<string>> DeleteContactAsync(string contactId);
        Response<IEnumerable<ContactUs>> GetAllContacts();
    }
}