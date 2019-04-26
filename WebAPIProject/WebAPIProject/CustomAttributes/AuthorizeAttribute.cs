﻿
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace WebAPIProject.CustomAttributes
{
    public class CustomAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

        }
    }
}