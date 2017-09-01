using Microsoft.Owin;
using Owin;

using Crawler;

[assembly: OwinStartupAttribute(typeof(LesGamblers.Web.Startup))]
namespace LesGamblers.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var crawler = new DataSeederEuroFinals2016();
            //var crawler = new DataSeederCL1617();
            var crawler = new DataSeederCL1718();
            crawler.CrawlTeamsData();
            ConfigureAuth(app);
        }
    }
}
