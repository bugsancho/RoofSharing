using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoofSharing.Web.Startup))]
namespace RoofSharing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
