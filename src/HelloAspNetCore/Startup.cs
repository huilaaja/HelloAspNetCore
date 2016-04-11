﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloAspNetCore.Services.HSL;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HelloAspNetCore
{
    public class Startup
    {

        public Startup()
        {
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHslConnector, HslConnector>();
            services.AddTransient<IHslRouteSolver, HslRouteSolver>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseIISPlatformHandler();            

            app.UseStaticFiles();

            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
           });

            //Domain specific initialization
            AddCoordinates();
            AddRouteOrigins();
    }

    protected void AddCoordinates()
    {
        HslCoordinateBank.AddCoordinatesFor(LocationEnum.Home, "2545370,6679959");
            HslCoordinateBank.AddCoordinatesFor(LocationEnum.KaroliinanWork, "2547800,6679751");
            HslCoordinateBank.AddCoordinatesFor(LocationEnum.TomminWork, "2552077,6673617");
    }

    protected void AddRouteOrigins()
    {
        RouteBank.Add(LocationEnum.Home, "Rautakiskonkuja 2");
        RouteBank.Add(LocationEnum.KaroliinanWork, "Pitäjänmäen peruskoulu");
        RouteBank.Add(LocationEnum.TomminWork, "Arkadianmäki");
    }

    // Entry point for the application.
    public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
