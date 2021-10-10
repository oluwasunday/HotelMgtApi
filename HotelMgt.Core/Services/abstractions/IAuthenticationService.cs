using HotelMgt.Dtos;
using HotelMgt.Dtos.AuthenticationDto;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IAuthenticationService
    {
        Task<Response<string>> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
        Task<Response<RegisterResponseDto>> RegisterUserAsync(RegisterDto model);
    }
}