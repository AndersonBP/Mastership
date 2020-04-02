using AutoMapper;
using Mastership.Application.Services;
using Mastership.Database.Repositories;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Context;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastership.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Application
            services.AddScoped<ICompanyApplication, CompanyApplication>();
            services.AddScoped<IEmployeeApplication, EmployeeApplication>();
            services.AddScoped<IPointTimeApplication, PointTimeApplication>();


            // Infra - Data
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPointTimeRepository, PointTimeRepository>();


            services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();
            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
                  opt.UseNpgsql(configuration.GetSection("ConnectionStrings:DefaultConnection").Value));
        }
    }
}
