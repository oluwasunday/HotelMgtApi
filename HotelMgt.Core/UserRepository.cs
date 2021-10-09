using AutoMapper;
using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using HotelMgt.Dtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgt.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelMgtDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(HotelMgtDbContext dbContext, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<AddUserResponseDto> AddUserAsync(AddUserDto user)
        {
            AppUser appUser = _mapper.Map<AppUser>(user);

            if (appUser == null)
            {
                throw new ArgumentNullException(nameof(appUser));
            }

            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "Customer");
                return _mapper.Map<AddUserResponseDto>(appUser);

            }

            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);
        }

        public async Task<IEnumerable<AddUserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users != null)
            {
                return _mapper.Map<IEnumerable<AddUserResponseDto>>(users);
            }
            throw new ArgumentException("User not found!");
        }

        public async Task<AddUserResponseDto> GetUserByIdAsync(string id)
        {

            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
                return _mapper.Map<AddUserResponseDto>(appUser);

            throw new KeyNotFoundException("User not found!");
        }


        public async Task<AddUserResponseDto> GetUserByEmailAsync(string email)
        {

            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null)
                return _mapper.Map<AddUserResponseDto>(appUser);

            throw new KeyNotFoundException("User not found!");
        }


        public async Task<AddUserResponseDto> UpdateUserAsync(string id, UpdateUserDto user)
        {
            

            var userInDb = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userInDb == null)
            {
                throw new ArgumentNullException("User does not exists");
            }

            userInDb.FirstName = user.FirstName;
            userInDb.LastName = user.LastName;
            userInDb.Email = user.Email;
            userInDb.PhoneNumber = user.PhoneNumber;

            await _userManager.UpdateAsync(userInDb);

            return _mapper.Map<AddUserResponseDto>(userInDb);
        }


        public async Task<bool> UpdateUserPictureAsync(AppUser user)
        {

            var userIdDb = _userManager.Users.FirstOrDefault(x => x.Id == user.Id);
            if (userIdDb == null)
            {
                throw new ArgumentNullException("User does not exists");
            }

            userIdDb.Avatar = user.Avatar;

            var result = await _userManager.UpdateAsync(userIdDb);
            if(result.Succeeded)
                return true;

            return false;
        }


        public IEnumerable<AddUserDto> SearchUsersByTerm(string searchTerm)
        {
            var user = _userManager.Users.Where(x =>
                x.FirstName.Contains(searchTerm) ||
                x.LastName.Contains(searchTerm) ||
                x.Email.Contains(searchTerm) ||
                x.PhoneNumber.Contains(searchTerm))
                .ToList();

            if (user != null)
                return _mapper.Map<List<AddUserDto>>(user);

            throw new KeyNotFoundException("No item found!");
        }
    }
}
