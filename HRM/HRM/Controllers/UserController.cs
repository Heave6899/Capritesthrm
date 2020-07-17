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
                int uid = (from s in context.User where s.UserName == model.UserName & s.Password == model.Password select s.UserId).First();
                if (isValid)
                {
                    if (uid != 0)
                    {
                        Session["UserID"] = uid;
                        var UserName = (from s in context.User where s.UserId == uid select s.UserName).First();
                        var RoleId = (from s in context.User where s.UserId == uid select s.RoleId).First();
                        Session["UserName"] = UserName;
                        Session["RoleId"] = RoleId;
                        FormsAuthentication.SetAuthCookie(UserName, true);
                        if (RoleId == 1)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (RoleId == 13)
                        {
                            return RedirectToAction("HRDashbord", "Home");
                        }
                        else if (RoleId == 19)
                        {
                            return RedirectToAction("EmployeeDashbord", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "User");
                    }
                }
                ModelState.AddModelError("", "Invalid username or Password");
                return View();

            }
        }

        public User GetUserDetails(int id)
        {
            try
            {
                User userDetails = new User();

                using (var db = new HRMEntities())
                {
                    userDetails = db.User.Where(c => c.UserId == id).Select(c => new User()
                    {
                        RoleId = c.RoleId,
                        UserName = c.UserName,
                    }).FirstOrDefault();
                }
                return userDetails;

            }
            catch (Exception)
            {
                throw;
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