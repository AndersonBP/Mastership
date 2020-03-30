using Mastership.Infra.Data.Context;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastership.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Application


            // Infra - Data

            services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();
            //services.AddEntityFrameworkNpgsql().AddScoped<DataContext>(opt =>
            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
           opt.UseNpgsql(configuration.GetSection("RhGestaoDb:ConnectionString").Value));
        }
    }
}
