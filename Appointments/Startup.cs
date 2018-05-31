using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Appointments.Startup))]
namespace Appointments
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
