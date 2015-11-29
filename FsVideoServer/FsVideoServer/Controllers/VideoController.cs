using Microsoft.AspNet.SignalR;
using FsVideoServer.Hubs;
using System.Web.Mvc;
using DbLibrary;
using FsVideoServer.Services;

namespace FsVideoServer.Controllers
{
    public class VideoController : Controller
    {
        private readonly IHubContext hubContext;
        public readonly IUserRepository repository; 

        public VideoController()
        {
            hubContext = GlobalHost.ConnectionManager.GetHubContext<VideoHub>();
            repository = new UserRepository();
        }

        [Route("video"), Route("~/", Name = "default"), Filters.AuthorizedFilter]
        public ActionResult Index(string url)
        {
            ViewBag.Url = "http://n25.filecdn.to/ff/M2IyNzRkNjNkZDRiNmQwYTM1YWJkOTcwMDkyNjc4ZjF8ZnN0b3wxMjk0OTc5MzMzfDEwMDAwfDJ8MHxufDI1fGE3NzEyOGM1NzE5MTRhMGU3YWMyNGRjMWI3ZGU0MDM2fDB8MTg6MC41OmN8MHwzNDQxMTM3MTN8MTQ0ODY1ODkxMC40ODM0/playvideo_673tbcyfq4vjzprqse04n8nzq.0.1765758616.2185543202.1448628451.mp4";

            var session = (UserSession)Session["UserSession"];
            ViewBag.UserName = session.UserName;

            return View(model:session);
        }

        [Route("play")]
        public ContentResult PlayVideo()
        {
            string connectionId = TempData["ConnectionId"] as string;

            hubContext.Clients.Client(connectionId).videoAction(new { action = "play" });
            return Content("play");
        }

        [Route("pause")]
        public ContentResult PauseVideo()
        {
            string connectionId = TempData["ConnectionId"] as string;

            hubContext.Clients.Client(connectionId).videoAction(new { action = "pause" });
            return Content("pause");
        }

        [Route("set-volume")]
        public ContentResult SetVolume(float value)
        {
            string connectionId = TempData["ConnectionId"] as string;

            if(value <= 1 && value >= 0)
                hubContext.Clients.Client(connectionId).videoAction(new { action = "setVolume", value = value });
            return Content("");
        }

        [Route("set-time")]
        public ContentResult SetTime(float value)
        {
            string connectionId = TempData["ConnectionId"] as string;

            if (value <= 1 && value >= 0)
                hubContext.Clients.Client(connectionId).videoAction(new { action = "setTime", value = value });
            return Content("");
        }

        [Route("error")]
        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}