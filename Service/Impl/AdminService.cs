using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Service.DTO_s;
using Service.Interface;
using Settings.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class AdminService : IAdmin
    {
        private readonly IUser _user;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserProfileModel> _userManager;
        private readonly IEmployer _employer;
        private readonly IGuarantor _guarantor;
        private readonly IHelper _helper;

        public AdminService(IUser user, ApplicationDbContext context, UserManager<UserProfileModel> userManager, IEmployer employer, IGuarantor guarantor, IHelper helper)
        {
            _user = user;
            _context = context;
            _userManager = userManager;
            _employer = employer;
            _guarantor = guarantor;
            _helper = helper;
        }

        public Task<bool> DisableUser(string userId)
        {
            return _user.DisableUserAsync(userId);
        }

        public Task<bool> EnableUser(string userId)
        {
            return _user.ActivateUserAsync(userId);
        }

        public async Task<CreateUserDTO> GetAdminById(string id)
        {
            if (id == null) return null;

            UserProfileModel user = await _userManager.FindByIdAsync(id);

            if (user == null) return null;

            CreateUserDTO userDto = new CreateUserDTO
            {
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,
                FirstName = user.FirstName,

                LastName = user.LastName,
                Surname = user.Surname,
                State = user.State,

            };

            return userDto;
        }

        public async Task<IEnumerable<CreateUserDTO>> GetAllAdmin()
        {
            var result =  _userManager.GetUsersInRoleAsync(ApplicationRoles.ADMIN);

            List<CreateUserDTO> users = new List<CreateUserDTO>();

            foreach(var user in await result)
            {
                users.Add(new CreateUserDTO {

                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Surname = user.Surname,
                    Email = user.Email,
                    Address = user.Address,
                    State = user.State,
                    PhoneNumber = user.PhoneNumber
                    
                });
            }
            return users;
        }

        public async Task<IEnumerable> GetAllEmployer()
        {
            return await _employer.GetAllAsync();
        }

        public async Task<IEnumerable> GetAllGuarantor()
        {
            return await _guarantor.GetAllAsync();
        }

        public async Task<IEnumerable> GetAllHelper()
        {
            return await _helper.GetAllAsync();
        }

        public async Task<EmployerDTO> GetEmployerById(int empId)
        {
            return  await _employer.GetByIdAsync(empId);
        }

        public async Task<GuarantorDTO> GetGuarantorById(int id)
        {
            return await _guarantor.GetByIdAsync(id);
        }

        public Task GetHelperById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
