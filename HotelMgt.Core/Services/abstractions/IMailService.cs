using HotelMgt.Dtos;
using HotelMgt.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IMailService
    {
        Task<Response<string>> SendEmailAsync(MailRequestDto mailRequest);
    }
}
