using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DealerConceptsApp.Startup))]
namespace DealerConceptsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
