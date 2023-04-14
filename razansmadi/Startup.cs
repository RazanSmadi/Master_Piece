using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(razansmadi.Startup))]
namespace razansmadi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
