using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoMaintenance.Startup))]
namespace AutoMaintenance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
