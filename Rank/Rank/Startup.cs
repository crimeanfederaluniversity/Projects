using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rank.Startup))]
namespace Rank
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
