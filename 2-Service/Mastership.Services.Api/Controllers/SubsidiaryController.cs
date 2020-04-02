using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Mastership.Services.Api.Controllers
{
    public class SubsidiaryController : ControllerBase
    {
        public SubsidiaryController(IPointTimeApplication service)
        {

        }

        [HttpPost]
        [Route("check.domainname")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CheckRegistration([FromBody]SubsidiaryViewModel obj)
        {
            return Ok("");
        }
    }
}