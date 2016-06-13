using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LesGamblers.Web.Startup))]
namespace LesGamblers.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
