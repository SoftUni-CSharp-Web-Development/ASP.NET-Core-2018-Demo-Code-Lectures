using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntroMvc.Data;
using IntroMvc.Filter;
using IntroMvc.ModelBinders;
using IntroMvc.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils.Messaging;

namespace IntroMvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = false;
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(1, 0, 0, 0);
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddDistributedSqlServerCache(options =>
                {
                    options.ConnectionString = this.Configuration.GetConnectionString("DefaultConnection");
                    options.SchemaName = "dbo";
                    options.TableName = "CacheData";
                });

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = new TimeSpan(0, 4, 0, 0);
            });

            services.AddMvc(
                options =>
                {
                   
                    // options.ModelBinderProviders.Insert(0, new DoNotUseModelBinderProvider());
                    // options.Filters.Add(new AddHeaderActionFilter());
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Application services
            services.AddScoped<IGreetingProvider, GreetingProvider>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<CounterService>();
            services.AddSingleton<MyResourceFilter>();
            services.AddResponseCompression(options => { options.EnableForHttps = true; });
            services.AddSingleton<ILogger, ConsoleLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Global.asax -> Application_Start, Application_Error
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "user",
                    template: "Users/{username}",
                    defaults: new {controller = "Home", action = "GetByUsername" });
                routes.MapRoute(
                    name: "blog",
                    template: "{year}/{month}/{day}",
                    defaults: new { controller = "Blog", action = "ByDate" },
                    constraints: new { year = @"\d{4}", month = @"\d{1,2}", day = @"\d{1,2}", });

                // routes.MapRoute()
            });
        }
    }
}
