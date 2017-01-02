using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AquavitBEAT.Startup))]
namespace AquavitBEAT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
