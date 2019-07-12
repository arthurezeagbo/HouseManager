using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Service.DTO_s;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class UserService : IUser
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<UserProfileModel> _userManager;
        protected readonly RoleManager<ApplicationRoleModel> _roleManager;

        public UserService(ApplicationDbContext context, UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> ActivateUserAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);

            if (user.IsActive) return true;

            user.IsActive = true;

            if (_userManager.UpdateAsync(user).GetAwaiter().GetResult().Succeeded)
                return true;

            return false;
        }

        public async Task<bool> DisableUserAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);

            if (!user.IsActive) return true;

            user.IsActive = false;

            if (_userManager.UpdateAsync(user).GetAwaiter().GetResult().Succeeded)
                return true;

            return false;
        }

        public async Task<bool> MarkUserAsDeletedAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);

            if (user.IsMarkedAsDeleted) return true;

            user.IsMarkedAsDeleted = true;

            if (_userManager.UpdateAsync(user).GetAwaiter().GetResult().Succeeded)
                return true; 

            return false;
        }
        
        public Task<bool> SendEmailToUserAsync(string address, string subject, object data)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserDTO> UpdateUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UpdateUserDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateUserAsync(CreateUserDTO model)
        {
            var user = new UserProfileModel
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                IsActive = true
            };

            if (!await _roleManager.RoleExistsAsync(model.UserType.ToString()))
                return "Invalid role";

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                var output = await _userManager.AddToRoleAsync(user, model.UserType.ToString());

                if (output.Succeeded)
                    return "User created successfully";
            }
            else
            {
                return  result.Succeeded.ToString();
            }

            return "Failed";
        }
        
    }
}
