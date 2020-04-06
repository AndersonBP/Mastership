using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        public IActionResult Register([FromBody]CheckRegistrationViewModel obj, [FromHeader(Name = "DomainName")][Required] string domainName)
        {
            return Ok(this.service.Register(obj, domainName));
        }
    }
}

