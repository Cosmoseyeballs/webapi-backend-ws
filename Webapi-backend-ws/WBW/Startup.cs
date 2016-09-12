using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WBW.Startup))]
namespace WBW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
