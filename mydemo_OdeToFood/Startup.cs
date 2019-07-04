using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mydemo_OdeToFood.Auth;
using OdeToFood.Data;

namespace mydemo_OdeToFood
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
            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFood"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                     .AddEntityFrameworkStores<OdeToFoodDbContext>();

            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            //This service is used in Api login and registration
            services.AddScoped<IAuthIdentity, ApiAuthenticationFeature>();

            services.Configure<IdentityOptions>(options =>
            {
                // This lambda enforce identity framework to take only unique email when registration
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizePage("/Restaurants/NewBooking");
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.Use(SayHelloMiddleware);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseNodeModules(env);
            app.UseCookiePolicy();

            app.UseMvc();
        }

        private RequestDelegate SayHelloMiddleware(
                                    RequestDelegate next)
        {
            return async ctx =>
            {

                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello, World!");
                }
                else
                {
                    await next(ctx);
                }
            };
        }
    }
}
