using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        static List<LoginModel> cty = new List<LoginModel>();
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel usermodel, string ReturnUrl)
        {
            using (motodbEntities db = new motodbEntities())
            {
                var obj = db.User_Records.Where(a => a.u_id == usermodel.u_id && a.u_pwd == usermodel.u_pwd).FirstOrDefault();


                if (obj != null)
                {
                    FormsAuthentication.SetAuthCookie(usermodel.u_id, false);
                    Session["UserID"] = obj.u_id.ToString();
                    Session["Username"] = obj.u_name.ToString();
                    Session["type"] = obj.u_type.ToString();
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        if (Session["type"].ToString() == "1")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Class_Record");
                        }

                    }

                }
                else
                {
                    var obj2 = db.Student_Records.Where(a => a.s_id == usermodel.u_id && a.s_pwd == usermodel.u_pwd).FirstOrDefault();
                    if (obj2 != null)
                    {
                        FormsAuthentication.SetAuthCookie(usermodel.u_id, false);
                        Session["UserID"] = obj2.s_id.ToString();
                        Session["Username"] = obj2.s_name.ToString();

                        return RedirectToAction("Index", "Class_Record");

                    }
                    else
                    {
                        ModelState.AddModelError("", "ID Pengguna atau Kata Laluan tidak sah");

                    }
                }

            }


            return View();
        }


    }
}
