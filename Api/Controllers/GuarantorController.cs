using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Service.DTO_s;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class GuarantorController : BaseController
    {
        public GuarantorController(IGuarantor guarantor) : base(guarantor)
        {
           
        }

        [HttpGet("GetAll")]
        public JsonResult GetAll()
        {
            return Json(_guarantor.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]
        public JsonResult GetById(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()) && id > 0)
                return Json(_guarantor.GetByIdAsync(id));

            return null;
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
