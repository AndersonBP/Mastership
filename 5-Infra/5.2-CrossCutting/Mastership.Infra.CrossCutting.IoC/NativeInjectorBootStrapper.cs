using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Application


            // Infra - Data
           
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<CovidContext>();
        }
    }
}
