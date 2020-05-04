using Hangfire;
using Hangfire.Dashboard;
using Mastership.Infra.CrossCutting.IoC;
using Mastership.Services.Api.Auth;
using Mastership.Services.Api.Configurations;
using Mastership.Services.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastership.Services.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private  SignInConfigurations signInConfigurations;
        private  JwtTokenOptions jwtTokenOptions;
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(env.ContentRootPath)
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register all DI
            BootStrapper.RegisterServices(services, Configuration); services.AddControllers();
            this.RegisterSingletons(services);

            services.AddOptions();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version:apiVersion}/"));
            });
            services.AddApiVersioning();

            this.ConfigureSwagger(services);
            this.ConfigureAuth(services);

            services
            .AddMvc()
            .AddNewtonsoftJson(o =>
            {
                //o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                o.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddApiVersioning();
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("*");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            app.UseMiddleware<FriendlyExceptionResponseMiddleware>();
            app.UseMiddleware<UserDataMiddleware>();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/rhgestao/swagger.json", "RhGestão API V1");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Will be available under http://localhost:5000/hangfire"
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new IDashboardAuthorizationFilter[]
                {
                    new HangfireDashboardJwtAuthorizationFilter(this.signInConfigurations, this.jwtTokenOptions, "Admin")
                }
            });
            app.UseHangfireServer();
            HangFireSchedule.ScheduleTasks(app.ApplicationServices);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                c.SwaggerDoc("rhgestao", new OpenApiInfo { Title = "RhGestão", Version = Configuration["Prefs:ApiVersion"] });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\n Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                            {
                              new OpenApiSecurityScheme
                              {
                                Reference = new OpenApiReference
                                  {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                  },
                                  Scheme = "oauth2",
                                  Name = "Bearer",
                                  In = ParameterLocation.Header
                                },
                                new List<string>()
                              } });
            });


        }

        private void RegisterSingletons(IServiceCollection services)
        {  // Authentication
            this.signInConfigurations = new SignInConfigurations();
            services.AddSingleton(this.signInConfigurations);

            this.jwtTokenOptions = new JwtTokenOptions();
            new ConfigureFromConfigurationOptions<JwtTokenOptions>(
                Configuration.GetSection("JwtTokenOptions"))
                    .Configure(this.jwtTokenOptions);
            services.AddSingleton(this.jwtTokenOptions);

        }
        private void ConfigureAuth(IServiceCollection services)
        {
        
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters.IssuerSigningKey = this.signInConfigurations.Key;
                bearerOptions.TokenValidationParameters.ValidAudience = this.jwtTokenOptions.Audience;
                bearerOptions.TokenValidationParameters.ValidIssuer = this.jwtTokenOptions.Issuer;
                // Valida a assinatura de um token recebido
                bearerOptions.TokenValidationParameters. ValidateIssuerSigningKey = true;
                // Verifica se um token recebido ainda é válido
                bearerOptions.TokenValidationParameters.ValidateLifetime = true;
                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                bearerOptions.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });


            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }


    }
}
