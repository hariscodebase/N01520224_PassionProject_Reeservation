using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(N01520224_PassionProject_Reeservation.Startup))]
namespace N01520224_PassionProject_Reeservation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
