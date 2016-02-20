using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chancelerry.Startup))]
namespace Chancelerry
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
