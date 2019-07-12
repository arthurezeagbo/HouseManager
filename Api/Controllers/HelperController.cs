using Microsoft.AspNetCore.Mvc;
using Service.DTO_s;
using Service.Interface;

namespace Api.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class HelperController : BaseController
    {
        public HelperController(IHelper helper) : base(helper)
        {

        }

        [HttpGet("GetAll")]
        public JsonResult GetAll()
        {
            return Json(_helper.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]
        public JsonResult GetById(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()) && id > 0)
                return Json(_helper.GetByIdAsync(id));

            return null;
        }

        [HttpPost("Update")]
        public JsonResult UpdateEmployer([FromBody] HelperDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                    return (Json(_helper.UpdateAsync(model).GetAwaiter().GetResult() ? "Done" : "Failed"));
            }

            return Json("Null value");
        }
    }
}
