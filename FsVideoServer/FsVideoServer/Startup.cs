using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FsVideoServer.Startup))]

namespace FsVideoServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
