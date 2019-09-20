using Data;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using Settings.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]
    //[Authorize(Roles = ""+ApplicationRoles.SUPER_ADMIN+","+ApplicationRoles.ADMIN+"")]
    public class AdminController : BaseController
    {
        private ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext context, UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager, IHelper helper, IGuarantor guarantor, IEmployer employer, ILogger<AdminController> logger)
            : base(context, userManager, roleManager, helper, guarantor, employer)
        {
            _logger = logger;
           
        }

        [HttpGet("GetAllGuarantors")]
        public async Task<JsonResult> GetAllGuarantors()
        {
            _logger.LogInformation(">>>> Getting all guarantor" );

            return Json(await _guarantor.GetAllAsync());
        }

        [HttpGet("GetGuarantorById/{id}")]
        public async Task<JsonResult> GetGuarantorById(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()) && id > 0)
                return Json(await _guarantor.GetByIdAsync(id));

            return null;
        }

        [HttpGet("GetAllEmployers")]
        public async Task<JsonResult> GetAllEmployers()
        {
            return Json(await _employer.GetAllAsync());
        }

        [HttpGet("GetEmployerById/{id}")]
        public async Task<JsonResult> GetEmployerById(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()) && id > 0)
                return Json(await _employer.GetByIdAsync(id));

            return null;
        }

        [HttpGet("GetAllHelper")]
        public async Task<JsonResult> GetAllHelpers()
        {
            return Json(await _helper.GetAllAsync());
        }

        [HttpGet("GetHelperById/{id}")]
        public async Task<JsonResult> GetHelperById(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()) && id > 0)
                return Json(await _guarantor.GetByIdAsync(id));

            return null;
        }

        public  async Task<JsonResult> DisableUser(string userId)
        {
            return null;
        }

        public async Task<JsonResult> EnableUser(string userId)
        {
            return null;
        }
       
    }
}
