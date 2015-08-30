using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrkIdea.QC.WebFront.Startup))]
namespace OrkIdea.QC.WebFront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
