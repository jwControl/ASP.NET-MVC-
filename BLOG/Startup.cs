using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BLOG.Startup))]
namespace BLOG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
