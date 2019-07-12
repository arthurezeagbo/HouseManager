﻿using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO_s;
using Service.Interface;
using Settings.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : BaseController
    {
        private IUser _user;
        private readonly SignInManager<UserProfileModel> _signInManager;

        public UserController(UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager, IUser user, SignInManager<UserProfileModel> signInManager) : base(userManager, roleManager)
        {
            _user = user;
            _signInManager = signInManager;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateUser(CreateUserDTO model)
        {
            if (ModelState.IsValid)
            {
                if (!model.PhoneNumber.Equals(model.ConfirmPassword))
                    return Json("Password mismatch");
                
                
            }

            return Json("Failed");
        }

        [Route("DeactivateUser")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> DeactivateUser(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return Json("Invalid user id");

            return Json(await _user.DisableUserAsync(userId));
        }

        [Route("ActivateUser")]
        [HttpPost]
        public async Task<JsonResult> ActivateUser(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return Json("Invalid user id");

            return Json(await _user.ActivateUserAsync(userId));
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid) return "Invalid model state";

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) return "User does not exist";

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,model.RememberMe, false);

            if (result.Succeeded)
            {
                return "User logged in";
            }

            return "Login failed";
        }

        [HttpPost]
        [Route("LogOut")]
        public async Task<string> LogOut()
        {
            await _signInManager.SignOutAsync();

            return "User signed out";
        }

    }
}