using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestCustomRulesWebApp.Startup))]
namespace TestCustomRulesWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
