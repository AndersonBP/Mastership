using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Mastership.Services.Api.Controllers
{
    public class SubsidiaryController : ControllerBase
    {
        private readonly ISubsidiaryApplication service;

        public SubsidiaryController(ISubsidiaryApplication service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("check.domainname")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CheckDomainName([FromBody]SubsidiaryViewModel obj)
        {
            return Ok(this.service.CheckDomainName(obj.DomainName));
        }
    }
}