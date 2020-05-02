using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QuickEmail.Utility
{

    public class UserAuthenticationFilter : ActionFilterAttribute, IActionFilter
    {



        public override void OnActionExecuted(ActionExecutedContext context)
        {
           

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;

            var email = context.HttpContext.Session.GetString("Email");
            var userName = context.HttpContext.Session.GetString("UserName");
            var UserId= context.HttpContext.Session.GetString("UserId");
            if (controller != null)
            {
                if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(UserId))
                {
                    context.Result =
                           new RedirectToRouteResult(
                               new RouteValueDictionary{{ "controller", "Admin" },
                                          { "action", "SignIn" }

                                                             });
                }
            }
            base.OnActionExecuting(context);

        }

    }
}
