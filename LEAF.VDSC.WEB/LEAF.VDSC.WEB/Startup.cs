﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LEAF.VDSC.CORE.Services;
using LEAF.VDSC.CORE.Dao;
using LEAF.VDSC.CORE.Config;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LEAF.VDSC.WEB
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
            services.AddMvc();

            // Setup cors
            services.AddCors();
                   
            /* Dependency Injection */
            // add service
            services.AddSingleton<IElandmallService, ElandmallService>();
            services.AddSingleton<IGmarketService, GmarketService>();
            services.AddSingleton<ITMonService, TMonService>();
            services.AddSingleton<IWemakepriceService, WemakepriceService>();

            // add dao
            services.AddSingleton<IElandmallDao, ElandmallDao>();
            services.AddSingleton<IGmarketDao, GmarketDao>();
            services.AddSingleton<ITMonDao, TMonDao>();
            services.AddSingleton<IWemakepriceDao, WemakepriceDao>();

            /* Configuration */
            // add basic configuration
            services.Configure<Connection>(Configuration.GetSection("Connection"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Setup cors
            app.UseCors(builder =>
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowCredentials()
            );

            

            // Reverse Proxy Setting >> For Nginx 
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseExceptionHandler(
                 builder =>
                 {
                     builder.Run(
                       async context =>
                       {
                           context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                           context.Response.ContentType = "application/json";

                           var error = context.Features.Get<IExceptionHandlerFeature>();
                           if (error != null)
                           {
                               var errorInfo = JsonConvert.SerializeObject(new { Message = error.Error.Message, StackTrace = error.Error.StackTrace,
                                                                        InnerException = error.Error.InnerException, Source = error.Error.Source });
                               await context.Response.WriteAsync(errorInfo);
                           }
                       });
                 }
            );


            app.UseMvc();
            app.UseAuthentication();

            
        }
    }
}
