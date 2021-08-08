using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PublisherBooks.Models;
using PagedList;
using MongoDB.Bson;


namespace PublisherBooks.Controllers
{
    public class UserDemandController : Controller
    {
        DataAccess DbContext;
     
        public ActionResult UserListDemand(string sortOrder, string currentFilter, string searchString, int? page)
        {
            DbContext = new DataAccess();
            if (DbContext.ConnectState.ToLower().Contains("error"))
            {
                Session["UserID"] = null;
                return RedirectToAction("Errordb", "Home");
            }
            if (Session["UserID"]!=null)//System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
                ViewBag.DateSortParm = sortOrder == "id" ? "Publisher" : "id";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
               
                var listbooks = from s in  DbContext.GetUserDemands(Session["UserID"].ToString())//System.Web.HttpContext.Current.User.Identity.Name) //DbContext._db.GetCollection<UserDemand>("UserDemand").FindAll()
                            select s;
                
                if (!String.IsNullOrEmpty(searchString))
                {
                    listbooks = listbooks.Where(s => s.UserDemandBook.Title.Contains(searchString)
                                           || s.UserDemandBook.Publisher.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "Title":
                        listbooks = listbooks.OrderByDescending(s => s.UserDemandBook.Title);
                        break;
                    case "Publisher":
                        listbooks = listbooks.OrderBy(s => s.UserDemandBook.Publisher);
                        break;
                    case "id":
                        listbooks = listbooks.OrderByDescending(s => s.Id);
                        break;
                    default:  // Name ascending 
                        listbooks = listbooks.OrderBy(s => s.UserDemandBook.Title);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(listbooks.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        public ActionResult Remove(string bkid, string usid)
        {
            DbContext = new DataAccess();
            if (DbContext.ConnectState.ToLower().Contains("error"))
            {
                return RedirectToAction("Errordb", "Home");
            }
            DbContext.RemoveUserDemand(bkid,usid);
            return RedirectToAction("UserListDemand", "UserDemand");
        }


    }
}
