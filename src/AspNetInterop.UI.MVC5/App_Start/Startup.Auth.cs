using System;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Interop;
using Owin;

namespace AspNetInterop.UI.MVC5
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var keyRingPath = Path.GetFullPath(Path.Combine(baseDirectory, "..", "AspNetInterop.KeyRing"));

            var protectionProvider = DataProtectionProvider.Create(new DirectoryInfo(keyRingPath));
            var dataProtector = protectionProvider.CreateProtector(
                "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                "Cookie",
                "v2");

            var ticketFormat = new AspNetTicketDataFormat(new DataProtectorShim(dataProtector));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookie",
                CookieName = ".AspNet.SharedCookie",
                TicketDataFormat = ticketFormat,
                LoginPath = new PathString("/Account/Login/"),

                // If you have subdomains use this config:
                CookieDomain = "localhost"
            });
        }
    }
}