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
        public PointTimeController(IPointTimeApplication service) { 
        
        }

        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CheckRegistration()
        {
            return  Ok("");
        }
    }
}

