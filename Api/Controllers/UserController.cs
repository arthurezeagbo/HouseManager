using Data.Model;
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
    //[Authorize]
    public class UserController : BaseController
    {
        private readonly IUser _user;
        private readonly SignInManager<UserProfileModel> _signInManager;

        public UserController(UserManager<UserProfileModel> userManager, RoleManager<ApplicationRoleModel> roleManager, IUser user, SignInManager<UserProfileModel> signInManager) : base(userManager, roleManager)
        {
            _user = user;
            _signInManager = signInManager;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<JsonResult> CreateUser([FromBody] CreateUserDTO model)
        {
            if (ModelState.IsValid)
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                    return Json("Password mismatch");

                var result = await _user.CreateUserAsync(model);

                return Json(result);
            }

            return Json("Failed");
        }

        [Route("UpdateUser/{userId}")]
        [HttpGet]
        public async Task<JsonResult> UpdateUser(string userId)
        {
            if (userId == null) return null;

            return Json(await _user.UpdateUserAsync(userId));
        }

        [Route("UpdateUser")]
        [HttpPost]
        public async Task<JsonResult> UpdateUser([FromBody] UpdateUserDTO userDetails)
        {
            if (userDetails == null) return null;

            return Json(await _user.UpdateUserAsync(userDetails));
        }

        [Route("DeactivateUser")]
        [HttpPost]
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

            if (!user.IsActive || user.IsMarkedAsDeleted)
                return "Your account has been deactivated. Contact Admin";

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,model.RememberMe, false);

            if (result.Succeeded)
                return "User logged in";

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
