using FsVideoServer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FsVideoServer.Filters
{
    public class AuthorizedFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies.Get("client");

            if (cookie != null)
            {
                // get user session id from cookies
                var sessionId = cookie.Value.ToString();
                var ctrl = (VideoController)filterContext.Controller;

                // get session from repository
                var session = ctrl.repository.Get(new Guid(sessionId));

                // if session not exists redirect to create session page
                if (session == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {{ "Controller", "Account" },
                                                   { "Action", "CreateSession" } });
                }
                else
                {
                    //save connection session into server session
                    HttpContext.Current.Session["UserSession"] = session;
                }
            }

            base.OnActionExecuting(filterContext);
        }

    }
}