using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustOrderPro.Models;
using CustOrderPro.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CustOrderPro
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the EF Core context with connection pooling for SQL Server database
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CustomerDBConnection")));
            services.AddIdentity<Customer, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            // Register MVC services in the DI container and configures the JSON serializer to use default contract resolver
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("Home//Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=120";
                }
            });

            app.UseSession();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
