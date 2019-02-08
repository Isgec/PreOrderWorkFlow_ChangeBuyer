using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PreOrderWorkFlow_ChangeBuyer.Startup))]
namespace PreOrderWorkFlow_ChangeBuyer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
