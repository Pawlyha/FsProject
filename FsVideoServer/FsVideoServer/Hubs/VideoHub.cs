using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FsVideoServer.Hubs
{
    [HubName("videoHub")]
    public class VideoHub : Hub
    {
        protected void Action(string message)
        {
            Clients.All.videoAction(message);
        }

    }

}