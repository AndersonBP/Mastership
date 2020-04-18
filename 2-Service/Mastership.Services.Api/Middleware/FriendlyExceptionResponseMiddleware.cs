using System;
using System.Net;
using System.Threading.Tasks;
using Mastership.Domain;
using Mastership.Domain.Exceptions;
using Mastership.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Mastership.Services.Api.Middleware
{
    public class FriendlyExceptionResponseMiddleware
    {

        private readonly RequestDelegate next;

        public FriendlyExceptionResponseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        //public async Task Invoke(HttpContext context, IUserDataService userDataService)
        {
            try
            {
                //context.Response.Headers.Add("X-RequestIdentity", userDataService.RequestIdentity.ToString());

                await next(context);
            }
            catch (Exception ex)
            {
                var message = "Erro Interno";
                var statusCode = HttpStatusCode.InternalServerError;

                //FIXME: Refatorar esses ifs
                if (ex is NotFoundException)
                {
                    message = ex.Message;
                    statusCode = HttpStatusCode.BadRequest;
                }
                else if (ex is ValidationException)
                {
                    message = ex.Message;
                    statusCode = HttpStatusCode.Unauthorized;
                }else if (ex is NetworkException)
                {
                    message = ex.Message;
                    statusCode = HttpStatusCode.NetworkAuthenticationRequired;
                }

                var data = new ExceptionResponseViewModel()
                {
                    Message = message,
                    RequestIdentity = new Guid()// userDataService.RequestIdentity
                };

                var responseJson = JsonConvert.SerializeObject(data);

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(responseJson);
            }
        }
    }
}
