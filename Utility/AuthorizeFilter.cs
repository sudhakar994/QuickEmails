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
            var verificationsuccess = context.HttpContext.Session.GetString("VerifictionSuccess");
            if (controller != null)
            {
                if (string.IsNullOrEmpty(verificationsuccess))
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
