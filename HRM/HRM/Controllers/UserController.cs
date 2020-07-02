using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HRM.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(User model)
        {
            using (var context = new HRMEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                { 
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or Password");
                return View();

            }
        }

        //////////[HttpPost]
        //////////public ActionResult Login(User model)
        //////////{
        //////////    int isValid = repository.CheckUser(model);
        //////////    if (isValid == 1 || isValid == 2)
        //////////    {
        //////////        FormsAuthentication.SetAuthCookie(model.Username, false);
        //////////        return RedirectToAction("Index", "Home");
        //////////    }

        //////////    ModelState.AddModelError("", "Invalid username/password");
        //////////    return View();
        //////////}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}