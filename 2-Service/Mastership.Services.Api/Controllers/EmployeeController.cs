using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mastership.Services.Api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : BaseController<EmployeeViewModel, IEmployeeApplication>
    {
        public EmployeeController(IEmployeeApplication service) : base(service) { }
    }
}

