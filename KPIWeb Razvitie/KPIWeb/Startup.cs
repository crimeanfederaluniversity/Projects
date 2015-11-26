using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KPIWeb.Startup))]
namespace KPIWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
