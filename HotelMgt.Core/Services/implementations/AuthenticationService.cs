using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Dtos.AuthenticationDto;
using HotelMgt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenGeneratorService _tokenGenerator;
        //private readonly IMailService _mailService;
        //private const string FilePath = "../HotelMgtAPI/StaticFiles/";


        public AuthenticationService(UserManager<AppUser> userManager, IMapper mapper, ITokenGeneratorService tokenGenerator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            //_mailService = mailService;
        }


        /// <summary>
        /// Create user and add to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response<RegisterResponseDto>> RegisterUserAsync(RegisterDto model)
        {
            string errors = "";
            var user = _mapper.Map<AppUser>(model);

            var responseDto = _mapper.Map<RegisterResponseDto>(user);
            if (model.Password != model.ConfirmPassword)
                return new Response<RegisterResponseDto>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Succeeded = false,
                    Message = "Failed!",
                    Data = responseDto,
                    Errors = errors
                };

            user.UserName = model.Email;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // TODO: send confirmation email
                await _userManager.AddToRoleAsync(user, "Customer");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return new Response<RegisterResponseDto>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Succeeded = true,
                    Message = "Successful!",
                    Data = responseDto,
                    Errors = errors
                };
            }

            errors = GetErrors(result);
            return new Response<RegisterResponseDto>()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Succeeded = false,
                Message = "Failed!",
                Data = responseDto,
                Errors = errors
            }; ;
        }


        public async Task<Response<LoginResponseDto>> LoginUserAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
                return new Response<LoginResponseDto>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Succeeded = false,
                    Message = "No user with the specified email address",
                    Data = new LoginResponseDto { Id = null, Token = null }
                };

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if(!result)
                return new Response<LoginResponseDto>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Succeeded = false,
                    Message = "Invalid password",
                    Data = new LoginResponseDto { Id = null, Token = null }
                };
            
            var token = await _tokenGenerator.GenerateToken(user);

            return new Response<LoginResponseDto>()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Login Successful",
                Succeeded = true,
                Data = new LoginResponseDto { Id = user.Id, Token = token }
            };
        }



        public async Task<Response<string>> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
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
