using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            //throw new NotImplementedException("Write a query to return all customers in the given city");
            //Select * From Customers WHERE City = 'x';
            var givenCity = _db.Customers.Where(c => c.City == city);
            return Ok(givenCity);
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            //throw new NotImplementedException("Write a query to return all customers from Mexico, Sweden and Germany.");
            //Select* FROM Customers Where Country IN ('MEXICO','Sweden','Germany');
            var selectedCountries = _db.Customers.Where(c => c.Country == "Mexico" || c.Country == "Sweden" || c.Country == "Germany");
            return Ok(selectedCountries);
        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            //throw new NotImplementedException("Write a query to return all customers with orders that shipped using the given shipperName.");
            //SELECT o.CustomerID, c.ContactName, c.Address, o.ShipVia  
            // FROM Orders o Inner Join Customers c
            //On o.CustomerID = c.CustomerID   , Id = c.CustomerID, name = ship.CompanyName, shipperName = o.ShipVia 
            //Where o.ShipVia = 1
            //var customers = _db.Customers;
            //var orders = _db.Orders;
            //var shippers = _db.Shippers;
            //var customerid = from c in customers
            //                 join o in orders on c.CompanyName equals o.ShipName
            //                 join ship in shippers on o.ShipVia equals ship.ShipperID
            //                 where 
            //                 select c;

            //return Ok(customerid.Distinct());
            var customerShip = _db.Customers
                               .Where(c => c.Orders
                                            .Any(o => o.Shipper.CompanyName == shipperName));

            return Ok(customerShip);

        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            //throw new NotImplementedException("Write a query to return all customers with no orders in the Orders table.");
            //Select c.ContactName, o.customerID, o.OrderID
            // From Customers c Inner Join Orders o
            //On o.CustomerID = c.CustomerID
            //Where o.OrderID IS NULL
            //var noOrders = _db.Customers.Select(c => c.CustomerID).Distinct();
            //var orderList = _db.Orders.Select(o => o.CustomerID).Distinct();
            //var final = noOrders.Except(orderList);
            //var x = _db.Customers.Where(o=> _db.Orders.CustomerID)


            var query =
                from c in _db.Customers
                where !(from o in _db.Orders
                        select o.CustomerID)
                        .Contains(c.CustomerID)
                select c;



            return Ok(query);
            //.Where(c => c.Orders
            //.Any(o => o.CustomerID != c.CustomerID));
            //return Ok(noOrders);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}