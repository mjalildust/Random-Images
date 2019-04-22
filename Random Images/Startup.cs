using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Random_Images.Startup))]
namespace Random_Images
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
