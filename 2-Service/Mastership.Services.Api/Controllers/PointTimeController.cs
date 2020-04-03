using Mastership.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Mastership.Services.Api.Controllers
{
    [Route("pointtime")]
    [ApiController]
    public class PointTimeController : ControllerBase
    {
        private readonly IPointTimeApplication service;
        public PointTimeController(IPointTimeApplication service) {
            this.service = service;
        }

        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CheckRegistration()
        {
            this.service.Register();
            return Ok(true);
        }
    }
}

