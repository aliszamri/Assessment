using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    using Assessment.Models;
    using System.Web.Security;

    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login usermodel, string ReturnUrl)
        {
                if (usermodel.Username != "alisalia2000@gmail.com")
                {
                    ModelState.AddModelError("Username", "Username does not belong to an account.");
                }
                else if (usermodel.Password != "Aliszamri178")
                {
                    ModelState.AddModelError("Password", "Password is incorrect.");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(usermodel.Username, false);
                    Session["Username"] = usermodel.Username.ToString();

                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Username"] = null;
            return RedirectToAction("Index", "Login");

        }
    }
}