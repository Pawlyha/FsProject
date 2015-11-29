using FsVideoServer.Models;
using System.Web.Mvc;
using AutoMapper;
using DbLibrary;
using FsVideoServer.Services;
using System;
using FsVideoServer.Filters;

namespace FsVideoServer.Controllers
{
    public class AccountController : Controller
    {

        public readonly IUserRepository repository; 

        public AccountController()
        {
            repository = new UserRepository();
        }

        [HttpGet, Route("create-session")]
        public ActionResult CreateSession(string videoUrl)
        {
            var model = new UserSession()
            {
                Id = Guid.NewGuid()
            };

            ViewBag.VideoUrl = videoUrl;
            return View(model);
        }

        [HttpPost, Route("create-session")]
        public ActionResult CreateSession(UserSession session, string videoUrl)
        {
            if (ModelState.IsValid)
	        {
                var result = repository.Create(session);

                Session["UserSession"] = session;
                HttpContext.Response.Cookies.Add(new System.Web.HttpCookie("client", session.Id.ToString()));

                return RedirectToAction("Index", "Video", new { url = videoUrl });
	        }

            return View(session);
        }
    }
}