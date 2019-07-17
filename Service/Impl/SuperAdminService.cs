using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [Authorize(Roles = ""+ApplicationRoles.SUPER_ADMIN+"")]

    public class SuperAdminService : ISuperAdmin
    {
        protected readonly IUser _user;
        private readonly IAdmin _admin;

        public SuperAdminService(IUser user, IAdmin admin)
        {
            _user = user;
            _admin = admin;
        }

        public async Task<bool> DisableAnyUserAsync(string userId)
        {
            bool result = await _user.DisableAnyUserAsync(userId);

            return result;
        }

        public async Task<bool> EnableAnyUserAsync(string userId)
        {
            bool result = await _user.ActivateAnyUserAsync(userId);

            return result;
        }

        public Task<IEnumerable<CreateUserDTO>> GetAllAdmin()
        {
            return _admin.GetAllAdmin();
        }

    }
}
