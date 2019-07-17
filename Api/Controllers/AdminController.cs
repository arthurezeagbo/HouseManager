﻿using Data;
using Data.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [DisableCors]
    public class AdminController : BaseController
    {
        public AdminController(ApplicationDbContext context, UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager, IHelper helper, IGuarantor guarantor, IEmployer employer)
            : base(context, userManager, roleManager, helper, guarantor, employer)
        {

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

        [HttpGet("GetAllHelpers")]
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
    }
}