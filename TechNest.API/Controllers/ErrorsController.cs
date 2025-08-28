using Microsoft.AspNetCore.Mvc;
using TechNest.API.Helper;

namespace TechNest.API.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet] 
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ResponseApi(statusCode));
        }
    }
}
