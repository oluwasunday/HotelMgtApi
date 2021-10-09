using AutoMapper;
using HotelMgt.Dtos;
using HotelMgt.Dtos.AuthenticationDto;
using HotelMgt.Models;
using MailKit;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class AuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        //private readonly ITokenGeneratorService _tokenGenerator;
        private readonly IMailService _mailService;
        private const string FilePath = "../HotelMgtAPI/StaticFiles/";


        public AuthenticationService(UserManager<AppUser> userManager,
            IMailService mailService, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            //_tokenGenerator = tokenGenerator;
            _mailService = mailService;
        }

        public async Task<Response<string>> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            var response = new Response<string>();
            if (user == null)
            {
                response.Message = "User not found";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;
            }
            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (result.Succeeded)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Email Confirmation successful";
                response.Data = user.Id;
                response.Succeeded = true;
                return response;
            }
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Message = GetErrors(result);
            response.Succeeded = false;
            return response;
        }

        private static string GetErrors(IdentityResult result)
        {
            return result.Errors.Aggregate(string.Empty, (current, err) => current + err.Description + "\n");
        }
    }
}
