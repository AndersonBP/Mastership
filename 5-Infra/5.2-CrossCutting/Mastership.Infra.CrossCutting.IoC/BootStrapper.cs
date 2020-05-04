﻿using AutoMapper;
using Hangfire;
using Hangfire.PostgreSql;
using Mastership.Application.Services;
using Mastership.Database.Repositories;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces;
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
using System;

namespace Mastership.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            RegisterServices(services);
            services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();

            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
                  opt.UseNpgsql(configuration.GetSection("ConnectionStrings:DefaultConnection").Value));
            services.Configure<EmailSettingsDTO>(x => configuration.GetSection("EmailSettings").Bind(x));

            services.AddHangfire(x =>
            x.UsePostgreSqlStorage(configuration.GetSection("ConnectionStrings:DefaultConnection").Value));
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserDataService, UserDataService>();

            // Application
            services.AddScoped<IBillingCustomerApplication, BillingCustomerApplication>();
            services.AddScoped<ICompanyApplication, CompanyApplication>();
            services.AddScoped<ICompanyIpRangesApplication, CompanyIpRangesApplication>();
            services.AddScoped<ICompanySettingsApplication, CompanySettingsApplication>();
            services.AddScoped<ISubsidiaryApplication, SubsidiaryApplication>();
            services.AddScoped<IEmployeeApplication, EmployeeApplication>();
            services.AddScoped<IPointTimeApplication, PointTimeApplication>();
            services.AddScoped<IUserApplication, UserApplication>();

            services.AddTransient<IEmailApplication, EmailApplication>();
            services.AddTransient<ITemplateService, TemplateService>();
            

            // Infra - Data
            services.AddScoped<IBillingCustomerRepository, BillingCustomerRepository>();
            services.AddScoped<ISubsidiaryRepository, SubsidiaryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyIpRangesRepository, CompanyIpRangesRepository>();
            services.AddScoped<ICompanySettingsRepository, CompanySettingsRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPointTimeRepository, PointTimeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddLazyResolution();

        }
    }
}
