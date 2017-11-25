using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebbyWeb.Models;

namespace WebbyWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the application.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HabitContext>(options =>
                                                options.UseSqlite("Data Source = Habit.db"));  //connecting

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HabitContext>()   //links user to DB
                .AddDefaultTokenProviders();    //default tokens allow temp access, useful for password reset, change password
            
            services.AddAuthentication()
                .AddCookie(options => {
                    options.LoginPath = "/Home/Index";
                    options.AccessDeniedPath = "/Home/Index";
                    options.LogoutPath = "/Home/Index/";
                });

            services.AddMvc();

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
                {

                    options.Cookie.HttpOnly = true;
                });
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
