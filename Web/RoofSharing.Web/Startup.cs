using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoofSharing.Web.Startup))]
namespace RoofSharing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var unityHubActivator = new MvcHubActivator();
            ConfigureAuth(app);
            app.MapSignalR();
              //GlobalHost.DependencyResolver.Register(
              //  typeof(IHubActivator), 
              //  () => unityHubActivator);
        }
    }

     public class MvcHubActivator : IHubActivator
        {
            public IHub Create(HubDescriptor descriptor)
            {
                return (IHub) DependencyResolver.Current
                    .GetService(descriptor.HubType);
            }
        }
}
