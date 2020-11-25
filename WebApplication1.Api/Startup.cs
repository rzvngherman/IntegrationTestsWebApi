using System;
using System.Collections.Generic;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication1.Api.Middleware;
using WebApplication1.Data.dataaccess;
using WebApplication1.Domain.Repository;
using WebApplication1.Service;
using WebApplication1.Service.Interfaces;
using WebApplication1.Service.Mapper;
using WebApplication1.Service.Query;

namespace WebApplication1.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();

            //test
            var dbConStr = Configuration.GetConnectionString("smap_IT_database");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => opt.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services
                .AddMediatR(typeof(GetEmployeeByNameQueryHandler).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", AddInfoData());
                c.IncludeXmlComments(SwaggerConfig.GetControllersXmlCommentsPath());
                //AddAuthenticationParameter(c);                
            });

            AddDbContext(services);

            MapInterfacesAndClasses(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalExceptionMiddleware();
            app.UseInvalidCommandExceptionMiddleware();

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                    swaggerDoc.Extensions.Add(new KeyValuePair<string, Microsoft.OpenApi.Interfaces.IOpenApiExtension>("x-logo2", new OpenApiString("http://integration_api.com/images/logo02.png")));
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Web Application1 API");
            });
        }

        private void MapInterfacesAndClasses(IServiceCollection services)
        {
            var mapper = ConfigureMapper.CreateMapper(new Mapper.WebApplication1Profile());
            services.AddSingleton(mapper);

            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IUnitOfWork>(s => new UnitOfWork(s.GetService<SomeDbContext>()));
        }

        private void AddDbContext(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("smap_IT_database");
            services.AddTransient<SomeDbContext>(provider =>
            {
                var builder = new DbContextOptionsBuilder<SomeDbContext>();
                builder.UseSqlServer(connection,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(5,
                                    TimeSpan.FromSeconds(15),
                                    errorNumbersToAdd: null);

                    }).EnableSensitiveDataLogging();
                return new SomeDbContext(builder.Options);
            });
        }

        private OpenApiInfo AddInfoData()
        {
            var info = new OpenApiInfo
            {
                Title = "Game Progress API",
                Version = "v1",
            };
            info.Extensions.Add(new KeyValuePair<string, Microsoft.OpenApi.Interfaces.IOpenApiExtension>("x-logo", new OpenApiString("http://integration_api.com/images/logo01.png")));
            return info;
        }

        private void AddAuthenticationParameter(SwaggerGenOptions c)
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    } },
                    new List<string>()
                }
            });

            //c.OperationFilter<AddHeaderParameter>();
        }
    }
}
