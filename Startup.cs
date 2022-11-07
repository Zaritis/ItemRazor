using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ItemRazor.Models;
using ItemRazor.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ItemRazor
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
            services.AddRazorPages();
            services.AddDbContext<ItemDbContext>();
            services.AddSingleton<ItemService, ItemService>();
            services.AddSingleton<UserService, UserService>();
            services.AddSingleton<OrderService, OrderService>();
            services.AddTransient<JsonFileService<Item>>();
            services.AddTransient<JsonFileService<User>>();
     //       services.AddSingleton<DbService, DbService>();
            services.AddTransient<DbGenericService<Item>, DbGenericService<Item>>();
           // services.AddTransient<DbGenericService<User>, DbGenericService<User>>();
            services.AddTransient<DbGenericService<Order>, DbGenericService<Order>>();
            services.AddTransient<UserDbService, UserDbService>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request. 
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions =>
            {
                cookieOptions.LoginPath = "/Login/LoginPage";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "admin"));
            });

            

            services.AddMvc().AddRazorPagesOptions(options =>
            {
              options.Conventions.AuthorizeFolder("/Item");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
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
                app.UseExceptionHandler("/Error");
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
            });
        }
    }
}
