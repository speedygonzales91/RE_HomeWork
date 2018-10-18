using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CurrencyEx.Startup))]
namespace CurrencyEx
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
