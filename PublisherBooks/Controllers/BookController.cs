using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherBooks.Models;
using PagedList;
using MongoDB.Bson;

namespace PublisherBooks.Controllers
{
    public class BookController : Controller
    {
        DataAccess DbContext;

        public BookController()
        {
            DbContext = new DataAccess();
            
        }
        // Action to display books , sort and search 
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
                DbContext = new DataAccess();
                if (DbContext.ConnectState.ToLower().Contains("error"))
                {
                  return  RedirectToAction("Errordb", "Home");
                }
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
                ViewBag.DateSortParm = sortOrder == "Authors" ? "Publisher" : "Authors";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var books = from s in DbContext._db.GetCollection<Book>("Book").FindAll()
                            select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    books = books.Where(s => s.Title.Contains(searchString)
                                           || s.Publisher.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "Title":
                        books = books.OrderByDescending(s => s.Title);
                        break;
                    case "Publisher":
                        books = books.OrderBy(s => s.Publisher);
                        break;
                    case "Authors":
                        books = books.OrderByDescending(s => s.Authors);
                        break;
                    default:  // Name ascending 
                        books = books.OrderBy(s => s.Title);
                        break;
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(books.ToPagedList(pageNumber, pageSize));
           
        }
        // This action to detail book and it recieve book id and return Book view to detail page
        // in this page detail button to add book to user list  demand if user is logged
        public ActionResult Details(string oid)
        {
            DbContext = new DataAccess();
            if (DbContext.ConnectState.ToLower().Contains("error"))
            {
                return RedirectToAction("Errordb", "Home");
            }

            ObjectId Id = new ObjectId(oid);
            Book book = DbContext.GetBookById(Id);
            return View(book);
        }
        // this action to add book to user list demands  and recieve two parameter 
        // oid -> book_id
        //username 
        public ActionResult DemandBook(string oid,string username)
        {
            DbContext = new DataAccess();
            if (DbContext.ConnectState.ToLower().Contains("error"))
            {
                return RedirectToAction("Errordb", "Home");
            }

            DbContext.CreateUserDemandBook(oid, username);
            return RedirectToAction("Index", "Book");
        }

        



    }
}
