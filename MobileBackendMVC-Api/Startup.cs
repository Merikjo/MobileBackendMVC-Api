using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MobileBackendMVC_Api.Startup))]
namespace MobileBackendMVC_Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
