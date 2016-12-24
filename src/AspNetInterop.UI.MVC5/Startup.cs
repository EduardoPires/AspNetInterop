using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetInterop.UI.MVC5.Startup))]
namespace AspNetInterop.UI.MVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
