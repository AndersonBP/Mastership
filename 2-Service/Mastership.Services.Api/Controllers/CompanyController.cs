using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Mastership.Services.Api.Controllers
{
    [Route("company")]
    [ApiVersion("1")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyApplication service;

        public CompanyController(ICompanyApplication service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("check.domainname")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CheckDomainName([FromBody]CompanyViewModel obj)
        {
            return Ok(this.service.CheckDomainName(obj.DomainName));
        }
    }
}