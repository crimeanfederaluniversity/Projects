using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EDM.Startup))]
namespace EDM
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
