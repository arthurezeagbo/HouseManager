using Data.Model;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    [Authorize(Roles = ""+ApplicationRoles.ADMIN+", "+ApplicationRoles.GUARANTOR+"")]
    public class GuarantorController : BaseController
    {
        public GuarantorController(IGuarantor guarantor) : base(guarantor)
        {
           
        }

       

        [HttpPost("Update")]
        public JsonResult UpdateEmployer(GuarantorDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                    return (Json(_guarantor.UpdateAsync(model).GetAwaiter().GetResult() ? "Done" : "Failed"));
            }

            return Json("Null value");
        }
    }
}
