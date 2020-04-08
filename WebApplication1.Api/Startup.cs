using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebApplication1.Data.dataaccess;
using WebApplication1.Domain.Repository;
using WebApplication1.Service;
using WebApplication1.Service.Interfaces;
using WebApplication1.Service.Mapper;

namespace WebApplication1.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Game Progress API", Version = "v1" });
                c.IncludeXmlComments(SwaggerConfig.GetControllersXmlCommentsPath());
                //AddAuthenticationParameter(c);
            });

            AddDbContext(services);

            MapInterfacesAndClasses(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
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
    }
}
