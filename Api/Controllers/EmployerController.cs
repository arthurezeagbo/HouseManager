using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO_s;
using Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployerController : BaseController
    {
        
        public EmployerController(IEmployer employer) : base(employer)
        {

        }

        [HttpGet("GetAll")]
        public JsonResult GetAll()
        {
            return Json(_employer.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]
        public JsonResult GetById(int id)
        {
            if(!string.IsNullOrEmpty(id.ToString()) && id > 0)
                return Json(_employer.GetByIdAsync(id));

            return null;
        }

        [HttpPost("Update")]
        public JsonResult UpdateEmployer(EmployerDTO model)
        {
            if (ModelState.IsValid)
            {
                if(model != null)
                    return (Json(_employer.UpdateAsync(model).GetAwaiter().GetResult() ? "Done" : "Failed"));
            }

            return Json("Null value");
        }
    }
}
