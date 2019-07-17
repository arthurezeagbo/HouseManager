using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Settings.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [DisableCors]
    [Authorize(Roles = ""+ApplicationRoles.SUPER_ADMIN+"")]
    public class SuperAdminController : BaseController
    {
        private readonly IAdmin _admin;

        public SuperAdminController(IUser user, IAdmin admin) : base(user)
        {
            _admin = admin;
        }

        [Route("DisableUser")]
        [HttpPost]
        public async Task<JsonResult> DisableUser(string id)
        {
            if (id == null) return null;

            return Json(await _user.DisableAnyUserAsync(id));
        }

        [Route("EnableUser")]
        [HttpPost]
        public async Task<JsonResult> EnableUser(string id)
        {
            if (id == null) return null;

            return Json(await _user.ActivateAnyUserAsync(id));
        }

        [Route("GetAllAdmin")]
        [HttpGet]
        public async Task<JsonResult> GetAllAdmin()
        {
            return Json(await _admin.GetAllAdmin());
        }

        [Route("GetAdminById")]
        [HttpGet]
        public async Task<JsonResult> GetAdminById(string id)
        {
            if (id == null) return Json("Enter admin Id");

            return Json(await _admin.GetAdminById(id));
        }

        public async Task<JsonResult> CreateAdmin()
        {
            return null;
        }
    }
}
