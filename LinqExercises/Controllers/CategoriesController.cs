using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CategoriesController : ApiController
    {
        private NORTHWNDEntities _db;

        public CategoriesController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: /api/categories
        [HttpGet, Route("api/categories"), ResponseType(typeof(IQueryable<Category>))]
        public IHttpActionResult GetAll()
        {
            //("Write a query to return all categories");
            //SELECT * From Categories
            //NORTHWIND db= new Northwind(@"C:Data\Northwind.mdf")
            //var query from c in db.customers select c;
            return Ok(_db.Categories);
        }

        //GET: /api/categories/search?term={term}
        [HttpGet, Route("api/categories/search"), ResponseType(typeof(IQueryable<Category>))]
        public IHttpActionResult Search(string term)
        {
            //("Write a query to return all categories where the category name contains the search term.");
            //SELECT * FROM CATEGORIES WHERE CategoryName Like '%%'
            var searchName = _db.Categories.Where(c => c.CategoryName.Contains(term));
            return Ok(searchName);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}