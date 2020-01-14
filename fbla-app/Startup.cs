using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fbla_app.Startup))]
namespace fbla_app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
