using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.CustomAttributes
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            string privilegeLevels = string.Join(";", GetPermissionByUser(httpContext.User.Identity.Name.ToString())); // Call another method to get rights of the user from DB

            if (privilegeLevels.Contains(this.AccessLevel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private List<string> GetPermissionByUser(string userName)
        {
            return new List<string>();
        }
    }
}