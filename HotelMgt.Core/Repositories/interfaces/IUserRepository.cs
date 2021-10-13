using HotelMgt.Dtos;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.interfaces
{
    public interface IUserRepository
    {
        Task<AddUserResponseDto> AddUserAsync(AddUserDto user);
        Task<IEnumerable<AddUserResponseDto>> GetAllUsersAsync();
        Task<AddUserResponseDto> GetUserByIdAsync(string id);
        Task<AddUserResponseDto> GetUserByEmailAsync(string email);
        Task<AddUserResponseDto> UpdateUserAsync(string id, UpdateUserDto user);
        Task<bool> UpdateUserPictureAsync(AppUser user);
        IEnumerable<AddUserDto> SearchUsersByTerm(string searchTerm);
    }
}