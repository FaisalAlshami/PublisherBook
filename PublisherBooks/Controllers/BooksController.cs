using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PublisherBooks.Models;

namespace PublisherBooks.Controllers
{
    [RoutePrefix("api/books")]
    /*
    Create a web service to allow querying of this data.
    this APIController as web service to query on books 
    and return result search as JSON Format
    */
    public class BooksController : ApiController
    {
        DataAccess DbContext;

        public BooksController()
        {
            DbContext = new DataAccess();
        }
        
        [HttpGet]
        public IHttpActionResult  GetBooks([FromUri] string search)
        {
            try
            {
                var books = from s in DbContext._db.GetCollection<Book>("Book").FindAll()
                            select s;
                if (!String.IsNullOrEmpty(search))
                {
                    // search in title , pulisher and decription
                    books = books.Where(s => s.Title.Contains(search)
                                           || s.Publisher.Contains(search)
                                           || s.Authors.Contains(search));
                }


                return this.Ok(books);
            }
            catch(Exception ex)
            {
                return this.Ok("Error to apply querying ");
            }
        }
        
       
    }
}
