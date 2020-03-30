using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mastership.Services.Api.Controllers
{
    [Route("api/pointtime")]
    [ApiController]
    public class PointTimeController 
    {
        public PointTimeController(IPointTimeApplication service) { }
    }
}

