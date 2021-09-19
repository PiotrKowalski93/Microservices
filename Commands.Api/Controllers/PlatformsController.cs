using Microsoft.AspNetCore.Mvc;
using System;

namespace Commands.Api.Controllers
{
    [ApiController]
    [Route("api/com/[controller]/[action]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
        }

        [HttpPost]
        public ActionResult Test()
        {
            Console.WriteLine("---> POST Worked");

            return Ok();
        }
    }
}
