﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using ProjectFood.Domain.Infrastructure;

namespace ProjectFood.WebUI.Infrastructure
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, int id)
        {
            AppUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();

            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }

        public static MvcHtmlString ClaimType(this HtmlHelper html, string claimType)
        {
            FieldInfo[] fields = typeof (ClaimTypes).GetFields();
            foreach (var field in fields)
            {
                if (field.GetValue(null).ToString() == claimType)
                {
                    return new MvcHtmlString(field.Name);
                }
            }
            return new MvcHtmlString(string.Format("{0}", claimType.Split('/', '.').Last()));
        }
    }
}