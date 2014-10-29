using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OSUTicketExchange.Web.Startup))]
namespace OSUTicketExchange.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
