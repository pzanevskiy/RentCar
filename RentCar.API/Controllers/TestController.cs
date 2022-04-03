using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentCar.API.Controllers
{
    [Authorize]
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("user")]
        [Authorize(Roles = Consts.User)]
        public IActionResult GetUser()
        {
            return Ok("Hello user");
        }

        [HttpGet("admin")]
        [Authorize(Roles = Consts.Admin)]
        public IActionResult GetAdmin()
        {
            return Ok("Hello admin");
        }

        [HttpGet("all")]
        [Authorize(Roles = "rentcar_admin, rentcar_user")]
        public IActionResult GetAll()
        {
            return Ok("Hello all");
        }
    }
}
