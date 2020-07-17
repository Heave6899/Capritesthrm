using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Linq;

namespace HRM.CustomFilters
{
    public class HRMActionAuthenticationService
    {
        //Check User Have Permission For this Action and Controller or not .
        public bool HRMActionAuthenticationFilter(string actionname, string controllername, int id)
        {
            bool IsPermission = false;

            try
            {
                using (var db = new HRMEntities())
                {
                    RolePermission rolePermission = new RolePermission();
                    rolePermission = db.RolePermission.Where(c => c.RoleId == id && c.IsPermission == true && c.ApplicationPermission.ControllerName == controllername && c.ApplicationPermission.ActionName == actionname && c.ApplicationPermission.PermissionMaster.PermissionName == "Read").FirstOrDefault();
                    if (rolePermission != null)
                    {
                        IsPermission = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return IsPermission;
        }
    }
    public class HRMAuthorization: ActionFilterAttribute, IAuthenticationFilter
    {
        public string permission { get; set; }
        HRMActionAuthenticationService hrmActionAuthenticationService = new HRMActionAuthenticationService();
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            int id = Convert.ToInt32(filterContext.HttpContext.Session["RoleId"].ToString());
            bool res = hrmActionAuthenticationService.HRMActionAuthenticationFilter(actionName, controllerName, id);
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserID"])) || res == false)
            {

                filterContext.Result = new HttpUnauthorizedResult();
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                       { "controller","Account"},
                       { "action","Login"},

                   });
            }
        }

    }

}