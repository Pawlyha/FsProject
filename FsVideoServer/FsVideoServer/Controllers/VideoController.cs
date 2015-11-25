using Microsoft.AspNet.SignalR;
using FsVideoServer.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FsVideoServer.Controllers
{
    public class VideoController : Controller
    {
        private readonly IHubContext hubContext;

        public VideoController()
        {
            hubContext = GlobalHost.ConnectionManager.GetHubContext<VideoHub>();
        }

        [Route("video"), Route("~/", Name="default")]
        public ActionResult Index(string url)
        {
            return View(model:url);
        }

        [Route("play")]
        public ContentResult PlayVideo()
        {
            hubContext.Clients.All.videoAction(new { action = "play"});
            return Content("");
        }

        [Route("pause")]
        public ContentResult PauseVideo()
        {
            hubContext.Clients.All.videoAction(new { action = "pause" });
            return Content("");
        }

        [Route("set-volume")]
        public ContentResult SetVolume(float value)
        {
            if(value <= 1 && value >= 0)
                hubContext.Clients.All.videoAction(new { action = "setVolume", value = value });
            return Content("");
        }

    }
}