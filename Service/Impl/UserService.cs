using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.DTO_s;
using Service.Interface;
using Settings.Constants;
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
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager, ILogger<UserService> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<bool> ActivateUserAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);

            if (_userManager.IsInRoleAsync(user, ApplicationRoles.GUARANTOR).Result || _userManager.IsInRoleAsync(user, ApplicationRoles.EMPLOYER).Result)
            {
                if (user.IsActive) return true;

                user.IsActive = true;

                if (_userManager.UpdateAsync(user).GetAwaiter().GetResult().Succeeded)
                    return true;
            }

            return false;
        }

        public async Task<bool> DisableUserAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            if(_userManager.IsInRoleAsync(user, ApplicationRoles.GUARANTOR).Result || _userManager.IsInRoleAsync(user, ApplicationRoles.EMPLOYER).Result)
            {
                if (!user.IsActive) return true;

                user.IsActive = false;

                if (_userManager.UpdateAsync(user).GetAwaiter().GetResult().Succeeded)
                    return true;
            }

            return false;
        }

        public async Task<bool> ActivateAnyUserAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);

            if (user.IsActive) return true;

            user.IsActive = true;

            if (_userManager.UpdateAsync(user).GetAwaiter().GetResult().Succeeded)
                return true;

            return false;
        }

        public async Task<bool> DisableAnyUserAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

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

        public async Task<UpdateUserDTO> UpdateUserAsync(string userId)
        {
            UserProfileModel user = await _userManager.FindByIdAsync(userId);//.GetAwaiter().GetResult();

            if (user == null) return null;

            UpdateUserDTO userDetails = new UpdateUserDTO
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                State = user.State
            };

            return  userDetails;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDTO data)
        {
            var user = _userManager.FindByIdAsync(data.UserId).Result;

            if (user == null) return false;

            user.FirstName = data.FirstName;
            user.LastName = data.FirstName;
            user.Surname = data.Surname;
            user.State = data.State;

            user.Address = data.Address;
            user.PhoneNumber = data.PhoneNumber;
            
            try
            {
                using(var context = _context)
                {
                    context.Attach(user);

                    await _context.SaveChangesAsync();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
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
                IsActive = true,
                Surname = model.Surname,
                Address = model.Address,
            };
            
            if (!await _roleManager.RoleExistsAsync(model.UserType))
                return "Invalid role";

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.UserType.ToString().Equals(ApplicationRoles.GUARANTOR))
                {
                    using (var context = _context)
                    {
                        GuarantorModel guarantor = new GuarantorModel
                        {
                            Address = model.Address,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            Gender = model.Gender,

                            LastName = model.LastName,
                            PhoneNumber1 = model.PhoneNumber,
                            State = model.State,
                            Surname = model.Surname,

                            User = user,
                            UserId = user.Id,
                        };

                        context.Guarantor.Add(guarantor);
                        await context.SaveChangesAsync();
                    }
   
                }
                else if (model.UserType.ToString().Equals(ApplicationRoles.EMPLOYER))
                {
                    using (var context = _context)
                    {
                        EmployerModel employer = new EmployerModel
                        {
                            Address = model.Address,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            Gender = model.Gender,
                            LastName = model.LastName,
                            PhoneNumber1 = model.PhoneNumber,
                            State = model.State,
                            Surname = model.Surname,
                            User = user,
                            UserId = user.Id,
                        };

                        context.Employer.Add(employer);
                        await context.SaveChangesAsync();
                    }
                   
                }
                
                var output = await _userManager.AddToRoleAsync(user, model.UserType.ToString());

                if (output.Succeeded)
                    return "User created successfully";
            }
            else
            {
                return  "Failed creating user";
            }

            return "Failed creating user";
        }
        
    }
}
