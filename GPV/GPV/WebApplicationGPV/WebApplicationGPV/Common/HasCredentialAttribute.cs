using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationGPV.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string mavaitro { set; get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (nguoidungLogin)HttpContext.Current.Session[Common.sessionSite.USER_SESSION];
            if (session == null)
            {
                return false;
            }

            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.MaNV);

            if (privilegeLevels.Contains(this.mavaitro) || session.PB == sessionLogin.ADMIN_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Home/Nofication.cshtml"
            };
        }

        private List<string> GetCredentialByLoggedInUser(int id)
        {
            var credentials = (List<string>)HttpContext.Current.Session[Common.sessionSite.SESSION_CAPQUYEN];
            return credentials;
        }
    }
}