﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace C.R.E.A.M.Filters
{
    public class AuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
           if(filterContext.HttpContext.Session["Username"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if(filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller", "Account" },
                    {"Action", "Login" }
                }
               );
            }
        }
    }
}