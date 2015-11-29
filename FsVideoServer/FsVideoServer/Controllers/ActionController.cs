using FsVideoServer.Filters;
using FsVideoServer.Hubs;
using Microsoft.AspNet.SignalR;
using System.Web.Http;

namespace FsVideoServer.Controllers
{
    public class ActionController : ApiController
    {
        private readonly IHubContext hubContext;

        public ActionController()
        {
            hubContext = GlobalHost.ConnectionManager.GetHubContext<VideoHub>();
        }

        [Route("api/video/play"), AuthorizedFilter]
        public IHttpActionResult PlayVideo()
        {
            string connectionId = (string)ActionContext.Request.Properties["ConnectionId"];

            hubContext.Clients.Client(connectionId).videoAction(new { action = "play" });
            return Ok("play");
        }

        [Route("api/video/pause"), AuthorizedFilter]
        public IHttpActionResult PauseVideo()
        {
            string connectionId = (string)ActionContext.Request.Properties["ConnectionId"];

            hubContext.Clients.Client(connectionId).videoAction(new { action = "pause" });
            return Ok("pause");
        }

        [Route("api/video/set-volume"), AuthorizedFilter]
        public IHttpActionResult SetVolume(float value)
        {
            string connectionId = (string)ActionContext.Request.Properties["ConnectionId"];

            if (value <= 1 && value >= 0)
                hubContext.Clients.Client(connectionId).videoAction(new { action = "setVolume", value = value });
            return Ok("volume");
        }

        [Route("api/video/set-time"), AuthorizedFilter]
        public IHttpActionResult SetTime(float value)
        {
            string connectionId = (string)ActionContext.Request.Properties["ConnectionId"];

            if (value <= 1 && value >= 0)
                hubContext.Clients.Client(connectionId).videoAction(new { action = "setTime", value = value });
            return Ok("time");
        }
    }
}
