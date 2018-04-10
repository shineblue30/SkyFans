using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SkyFan.Startup))]
namespace SkyFan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
