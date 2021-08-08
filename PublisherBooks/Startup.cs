using Microsoft.Owin;
using Owin;
using PublisherBooks.Models;

[assembly: OwinStartupAttribute(typeof(PublisherBooks.Startup))]
namespace PublisherBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           DataAccess DbContext = new DataAccess();
            if (DbContext.ConnectState.ToLower().Contains("error"))
            {
               
            }
            //   ConfigureAuth(app);
        }
    }
}
