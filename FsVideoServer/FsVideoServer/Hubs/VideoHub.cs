using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using DbLibrary;
using FsVideoServer.Services;

namespace FsVideoServer.Hubs
{
    [HubName("videoHub")]
    public class VideoHub : Hub
    {

        private IUserRepository _repository;

        public VideoHub()
        {
            _repository = new UserRepository();
        }

        // attach signalR connectionId to userSession
        public override System.Threading.Tasks.Task OnConnected()
        {
            // this value was retrieved from cookies on the client side
            var sessionId = this.Context.QueryString["UserSessionId"].ToString();
            var session = _repository.Get(new Guid(sessionId));

            if (session != null)
            {
                // renew signal session Id
                session.ConnectionId = this.Context.ConnectionId;

                // update user  session
                var result = _repository.Update(session);
            }

            return base.OnConnected();
        }
    }

}