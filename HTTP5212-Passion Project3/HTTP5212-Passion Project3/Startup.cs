using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTTP5212_Passion_Project3.Startup))]
namespace HTTP5212_Passion_Project3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
