using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SummerFreshFlowers.Startup))]
namespace SummerFreshFlowers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
