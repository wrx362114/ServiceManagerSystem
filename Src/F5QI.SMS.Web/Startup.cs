using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F5QI.SMS.Web.Startup))]
namespace F5QI.SMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
