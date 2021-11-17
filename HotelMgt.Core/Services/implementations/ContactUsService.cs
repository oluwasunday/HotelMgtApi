using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.ContactUsDtos;
using HotelMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<ContactUs>> AddContactUsAsync(AddContactUsDto contactUsDto)
        {
            var contact = _mapper.Map<ContactUs>(contactUsDto);

            await _unitOfWork.ContactUs.AddAsync(contact);
            await _unitOfWork.CompleteAsync();

            return Response<ContactUs>.Success("success", contact);
        }

        public async Task<Response<string>> DeleteContactAsync(string contactId)
        {
            var contact = await _unitOfWork.ContactUs.GetAsync(contactId);

            if (contact == null)
                return Response<string>.Fail("Contact not found");

            _unitOfWork.ContactUs.Remove(contact);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("success", "contact deleted");
        }

        public Response<IEnumerable<ContactUs>> GetAllContacts()
        {
            var contacts = _unitOfWork.ContactUs.GetAll();
            return Response<IEnumerable<ContactUs>>.Success("success", contacts);
        }
    }
}
