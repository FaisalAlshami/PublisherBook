using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherBooks.Models;

namespace PublisherBooks.Controllers
{
    public class AccountController : Controller
    {
        DataAccess DbContext;
        public ActionResult Index()
        {
            return View();
        }
        // This controller to change password user
        // Use ChangePassword class to confirmation password enter by user in page
        public ActionResult Changepwd()
        {
            if(Session["UserID"]!=null)
            {
                ChangePassword chpwd = new ChangePassword();
                chpwd.UserName = Session["UserID"].ToString();
                chpwd.ChangeState = "no";
                return View(chpwd);

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        //this action recieve changepassword parameter and copy password to user and save change in db
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Changepwd(ChangePassword model)
        {
            DbContext = new DataAccess();
            if (DbContext.ConnectState.ToLower().Contains("error"))
            {
                return RedirectToAction("Errordb", "Home");
            }
            // validation input user
            if (ModelState.IsValid)
            {
                try
                {
                    
                    DbContext.ChangepasswordUser(model.UserName, model.Password);
                    model.ChangeState = "ok";
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
        }

    }
}