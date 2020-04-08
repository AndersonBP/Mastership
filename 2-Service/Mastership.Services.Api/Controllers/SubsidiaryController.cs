using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Mastership.Domain.ViewModels.RequestResponseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace Mastership.Services.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("subsidiary")]
    [ApiVersion("1")]
    [ApiController]
    public class SubsidiaryController : ControllerBase
    {
        private readonly ISubsidiaryApplication service;

        public SubsidiaryController(ISubsidiaryApplication service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("create.afd")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateAFD([FromBody]AFDViewModel obj)
        {
            return Ok(this.service.CreateAFD(obj));
        }
    }
}