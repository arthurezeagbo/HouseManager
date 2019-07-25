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
    [Authorize(Roles = ""+ApplicationRoles.SUPER_ADMIN+","+ApplicationRoles.ADMIN+"")]
    public class AdminController : BaseController
    {
        private static Action<ILogger,string, Exception> _logMessage;
        private ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext context, UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager, IHelper helper, IGuarantor guarantor, IEmployer employer, ILogger<AdminController> logger)
            : base(context, userManager, roleManager, helper, guarantor, employer)
        {
            _logger = logger;
            var a = MethodBase.GetCurrentMethod().Name;
        }

        [HttpGet("GetAllGuarantors")]
        public async Task<JsonResult> GetAllGuarantors()
        {
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
            var a = MethodBase.GetCurrentMethod().Name;
            LogMessage(_logger, this.GetType(), MethodBase.GetCurrentMethod().Name, "GET all helpers");

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

        public static void LogMessage(ILogger logger,Type type, string actionName, string message)
        {
           
            _logMessage = LoggerMessage.Define<string>(LogLevel.Information,  new EventId(2, actionName), $"{type}    {actionName} Log =========>>>   "+message);

            var b = _logMessage;
        }

       
    }
}
