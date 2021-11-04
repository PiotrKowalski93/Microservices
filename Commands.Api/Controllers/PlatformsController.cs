using AutoMapper;
using Commands.Domain.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Commands.Api.Controllers
{
    [ApiController]
    [Route("api/com/[controller]/[action]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Test()
        {
            Console.WriteLine("---> POST Worked");

            return Ok();
        }
    }
}
