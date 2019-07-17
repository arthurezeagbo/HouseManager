using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class SuperAdminController : BaseController
    {
        public SuperAdminController()
        {

        }

        public async Task<JsonResult> DisableAdmin(int id)
        {
            return null;
        }

        public async Task<JsonResult> EnableAdmin(int id)
        {
            return null;
        }

        public async Task<JsonResult> GetAllAdmin()
        {
            return null;
        }

        public async Task<JsonResult> CreateAdmin()
        {
            return null;
        }
    }
}
