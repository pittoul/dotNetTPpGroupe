using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPLibrairiev03.Abstraction;
using TPLibrairiev03.EFClasses;
using TPLibrairiev03;
using LibrairieDB;
using Microsoft.EntityFrameworkCore;

namespace TPLibrairieEnGroupe
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
            services.AddControllersWithViews();

            // Je configure une injection de dépendances :
            // Lorsque le système veut un IIdentityFormater
            // Je lui donne un StudentIdentityFormater
            services.AddScoped<IArticleRepository, ArticleRepoEF>();

            services.AddDbContext<LibrairieDbContext>(options => options.UseSqlite("Filename=librairie_tp_dot_net.db"));



            /*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           {
               // définition de la base de données à utiliser ainsi que de la chaine de connexion
               optionsBuilder.UseSqlite("Filename=librairie_tp_dot_net.db");

               base.OnConfiguring(optionsBuilder);
           } */
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
