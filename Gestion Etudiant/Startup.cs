﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gestion_Etudiant.Data;
using Microsoft.AspNetCore.Identity;

namespace Gestion_Etudiant
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

            services.AddDbContext<Gestion_EtudiantContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Gestion_EtudiantContext")));

            services.AddAuthentication()
               .AddGoogle(options =>
               {
                   options.ClientId = Configuration["App:GoogleClientId"];
                   options.ClientSecret = Configuration["App:GoogleClientSecret"];
               })
               .AddFacebook(options =>
               {
                   options.AppId = Configuration["App:FacebookClientId"];
                   options.ClientSecret = Configuration["App:FacebookClientSecret"];
                   options.AccessDeniedPath = "/AccessDeniedPathInfo";
               });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
