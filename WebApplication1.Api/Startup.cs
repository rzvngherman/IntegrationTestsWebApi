using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Data.dataaccess;
using WebApplication1.Data.domain;
using WebApplication1.Data.service;

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
            
            services.AddScoped<IEmployeeService, EmployeeService>();

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
                }
               );

            services.AddScoped<IUnitOfWork>(s => new UnitOfWork(s.GetService<SomeDbContext>()
                ));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
