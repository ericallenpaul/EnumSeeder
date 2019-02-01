using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnumSeeder.Models;
using EnumSeeder.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;

namespace EnumSeeder.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services
                .AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(connection))
                .AddTransient<IRepository, EFRepository<ApplicationDbContext>>()
                .AddOptions()
                .Configure<EnumSeeder_Settings>(Options => Configuration.GetSection("EnumSeeder_Settings").Bind(Options))
                .AddSingleton<IConfiguration>(Configuration)
                .AddSwaggerGen(SwaggerHelper.ConfigureSwaggerGen)
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddMemoryCache();

            services.AddCors(
                o => o.AddPolicy(
                    "MyPolicy", builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }));

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory LoggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });
            app.UseSwagger(SwaggerHelper.ConfigureSwagger);
            app.UseSwaggerUI(SwaggerHelper.ConfigureSwaggerUI);
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseExceptionHandler("/error/500");
            app.UseMvc();
            app.UseStaticFiles();
            //env.ConfigureNLog("nlog.config");
            LoggerFactory.AddNLog();
            app.AddNLogWeb();

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
