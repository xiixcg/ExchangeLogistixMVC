using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExchangeLogistixMVC.Startup))]
namespace ExchangeLogistixMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
