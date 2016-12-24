using System.IO;
using AspNetInterop.UI.Core.Data;
using AspNetInterop.UI.Core.Models;
using AspNetInterop.UI.Core.Permissions;
using AspNetInterop.UI.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetInterop.UI.Core
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
                builder.AddUserSecrets();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            _hostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var contentRoot = _hostingEnvironment.ContentRootPath;
            var keyRingPath = Path.GetFullPath(Path.Combine(contentRoot, "..", "aspNetInterop.KeyRing"));

            var protectionProvider = DataProtectionProvider.Create(new DirectoryInfo(keyRingPath));
            var dataProtector = protectionProvider.CreateProtector(
                "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                "Cookie",
                "v2");

            var ticketFormat = new TicketDataFormat(dataProtector);

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Cookies = new IdentityCookieOptions
                    {
                        ApplicationCookie = new CookieAuthenticationOptions
                        {
                            AuthenticationScheme = "Cookie",
                            CookieName = ".AspNet.SharedCookie",
                            TicketDataFormat = ticketFormat,

                            LoginPath = new PathString("/Account/Login/"),
                            AccessDeniedPath = new PathString("/Error/Forbidden/"),
                            AutomaticAuthenticate = true,
                            AutomaticChallenge = true,

                            // If you have subdomains use this config:
                            CookieDomain = "localhost"
                        }
                    };
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Write")));
                options.AddPolicy("CanAccessCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Access")));
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}