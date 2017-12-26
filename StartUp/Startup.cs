using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StartUp.Startup))]
namespace StartUp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
