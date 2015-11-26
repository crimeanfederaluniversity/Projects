using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalPages.Startup))]
namespace PersonalPages
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
