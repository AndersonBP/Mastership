using Mastership.Domain.Interfaces;
using Mastership.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mastership.Services.Api.Middleware
{
    public class UserDataMiddleware
    {
        private readonly RequestDelegate next;

        public UserDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IUserDataService userData)
        {
            var auth = context.Request.Headers["Authorization"];
            if (context.Request.Method != "OPTIONS" && !string.IsNullOrEmpty(auth))
            {
                var token = context.Request.GetToken();
                userData.Load(token.Lookup());
            }

            await next(context);
        }
    }
}
