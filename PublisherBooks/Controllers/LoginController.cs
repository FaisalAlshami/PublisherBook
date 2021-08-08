using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherBooks.Models;
using System.Web.Security;


namespace PublisherBooks.Controllers
{
    public class LoginController : Controller
    {

        DataAccess DbContext;
 
        public LoginController()
        {
            DbContext = new DataAccess();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        // this to valid data login 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            
            if (ModelState.IsValid)
            {
                DbContext = new DataAccess();
                if (DbContext.ConnectState.ToLower().Contains("error"))
                {
                    return RedirectToAction("Errordb", "Home");
                }
                User objUser = new User();
                objUser.Username = model.Username;
                objUser.Password = model.Password;
                var obj = DbContext.CheckLogin(objUser.Username, objUser.Password);
                if (obj != null)
                {
                    Session["UserID"] = objUser.Username;
                    return RedirectToAction("UserListDemand", "UserDemand");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect .");

                }
            }
            return View(model);
            //return RedirectToAction("Login","Login");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}
