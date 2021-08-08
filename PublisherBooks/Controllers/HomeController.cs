using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublisherBooks.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        // this action to display if can't connected to DB
        public ActionResult Errordb()
        {
            return View();
        }

    }
}
