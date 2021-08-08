using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherBooks.Models;
using System.Web.Security;

namespace PublisherBooks.Controllers
{
    public class RegisterController : Controller
    {
        DataAccess DbContext ;

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            DbContext = new DataAccess();
            if (DbContext.ConnectState.ToLower().Contains("error"))
            {
                return RedirectToAction("Errordb", "Home");
            }
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    if (model.UserName.Contains(" ") == false)
                    {
                        User userexist = DbContext.CheckUsernameExist(model.UserName);
                        if (userexist == null)
                        {
                            User usr = new Models.User();
                            usr.Username = model.UserName;
                            usr.Password = model.Password;
                            if (DbContext.CreateUser(usr) != null)
                            {
                                FormsAuthentication.SetAuthCookie(model.UserName, false);
                                Session["UserID"] = model.UserName;
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("", "don't create account , Please again .");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", " Please enter a different username without space .");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
        }
    }
}
