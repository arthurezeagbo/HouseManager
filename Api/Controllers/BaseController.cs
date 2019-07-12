using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Impl;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<UserProfileModel> _userManager;
        protected readonly RoleManager<ApplicationRoleModel> _roleManager;
        protected readonly IHelper _helper;
        protected readonly IGuarantor _guarantor;
        protected readonly IEmployer _employer;
        
        public BaseController()
        {

        }

        public BaseController(IHelper helper)
        {
            _helper = helper;
        }

        public BaseController(IEmployer employer )
        {
            _employer = employer;
        }

        public BaseController(IGuarantor guarantor )
        {
            _guarantor = guarantor;
        }

        public BaseController(ApplicationDbContext context, UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public BaseController(UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}
