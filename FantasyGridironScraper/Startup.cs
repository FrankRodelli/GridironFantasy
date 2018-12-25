using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using FantasyGridironSite.Models;

namespace FantasyGridironScraper
{
    public class Startup
    {
        List<Player> theThing;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var scraper = new ScrapePlayerData();
            theThing = scraper.GetSomething();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
               foreach (var player in theThing)
                {
                    try
                    {
                        await context.Response.WriteAsync(player.YHId + "\n");
                    }
                    catch
                    {

                    }
                }
            });
        }
    }
}
