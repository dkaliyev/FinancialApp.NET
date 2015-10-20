using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinancialThing.Startup))]
namespace FinancialThing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
