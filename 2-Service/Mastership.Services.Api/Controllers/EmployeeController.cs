using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Mastership.Services.Api.Controllers
{
    [Route("employee")]
    [ApiVersion("1")]
    [ApiController]
    public class EmployeeController : BaseController<EmployeeViewModel, IEmployeeApplication>
    {
        public EmployeeController(IEmployeeApplication service) : base(service) { }

        [HttpPost]
        [Route("check.registration")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CheckRegistration([FromBody]EmployeeViewModel obj)
        {
            return Ok(this.Service.CheckRegistration(obj, Request.GetHeader("DomainName")));
        }

    }
}

