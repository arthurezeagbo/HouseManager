﻿using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

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
        protected readonly IUser _user;

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

        public BaseController(IUser user)
        {
            _user = user;
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

        public BaseController(ApplicationDbContext context, UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager, IHelper helper, IGuarantor guarantor, IEmployer employer)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _helper = helper;
            _guarantor = guarantor;
            _employer = employer;
        }
    }
}
