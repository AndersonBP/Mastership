using Mastership.Application.ViewModels;
using Mastership.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace Mastership.Services.Api.Controllers
{
    [ApiController]
    [Route("api/employee")]
    //[Authorize(Policy = Polices.Administracao)]
    public class EmployeeController : BaseController<EmployeeViewModel, IEmployeeApplication>
    {
        public EmployeeController(IEmployeeApplication service) : base(service) { }

    }
}