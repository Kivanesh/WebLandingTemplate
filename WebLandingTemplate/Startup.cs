using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebLandingTemplate.Startup))]
namespace WebLandingTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
