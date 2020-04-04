using AutoMapper;
using Mastership.Application.Services;
using Mastership.Database.Repositories;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Infra.CrossCutting.Extensions;
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
            services.AddScoped<IBillingCustomerApplication, BillingCustomerApplication>();
            services.AddScoped<ICompanyApplication, CompanyApplication>();
            services.AddScoped<ISubsidiaryApplication, SubsidiaryApplication>();
            services.AddScoped<IEmployeeApplication, EmployeeApplication>();
            services.AddScoped<IPointTimeApplication, PointTimeApplication>();
            services.AddScoped<IUserApplication, UserApplication>();


            // Infra - Data
            services.AddScoped<IBillingCustomerRepository, BillingCustomerRepository>();
            services.AddScoped<ISubsidiaryRepository, SubsidiaryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPointTimeRepository, PointTimeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddLazyResolution();

            services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();
            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
                  opt.UseNpgsql(configuration.GetSection("ConnectionStrings:DefaultConnection").Value));
        }
    }
}
