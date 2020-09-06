using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(V1_1.Startup))]
namespace V1_1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
