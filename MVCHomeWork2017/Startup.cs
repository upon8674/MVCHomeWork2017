using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCHomeWork2017.Startup))]
namespace MVCHomeWork2017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
