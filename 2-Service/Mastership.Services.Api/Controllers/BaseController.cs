using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace Mastership.Services.Api.Controllers
{

    [ApiExplorerSettings(GroupName = "rhgestao")]
    public abstract class BaseController<TVM, TService> : ControllerBase where TVM : BaseViewModel, new() where TService : IApplication<TVM>
    {
        protected readonly TService Service;

        public BaseController(TService service)
            => Service = service;

        [HttpGet]
        [Route("odata")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Expand | AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top)]
        public IActionResult Get(ODataQueryOptions<TVM> opts)
        {
            IQueryable<TVM> list = Service.List();

            var count = Service.Count(opts);

            Response.Headers.Add("X-Count", $"{count}");
            Response.Headers.Add("X-Now", $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}");

            return Ok(list);
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Adicionar([FromBody]TVM objDto)
        {
            var obj = Service.Add(objDto);

            return Ok(obj);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Buscar(Guid id)
        {
            var obj = Service.Search(id);

            return Ok(obj);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Remover(Guid id)
        {
            Service.Disable(id);

            return Ok();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Editar(Guid id, [FromBody]TVM objDto)
        {
            var obj = Service.Update(id, objDto);

            return Ok(obj);
        }
    }
}