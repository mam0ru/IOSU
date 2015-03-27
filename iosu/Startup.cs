using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iosu.Startup))]
namespace iosu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
