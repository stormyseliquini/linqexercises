﻿using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class OrdersController : ApiController
    {
        private NORTHWNDEntities _db;

        public OrdersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/orders/between/01.01.1997/12.31.1997
        [HttpGet, Route("api/orders/between/{startDate}/{endDate}"), ResponseType(typeof(IQueryable<Order>))]
        public IHttpActionResult GetOrdersBetween(DateTime startDate, DateTime endDate)
        {
            //throw new NotImplementedException("Write a query to return all orders with required dates between the given start date and the given end date with freight under 100 units.");
            //SELECT o.CustomerID, o.RequiredDate, o.Freight From Orders o WHERE o.RequiredDate BETWEEN '1997-01-01' AND '1998-01-01' AND o.Freight < '100'

            var query = from o in _db.Orders
                        where (o.RequiredDate >= startDate && o.RequiredDate <= endDate) && (o.Freight < 100)
                        select o;
            return Ok(query);
        }

        //GET: api/orders/reports/purchase
        [HttpGet, Route("api/orders/reports/purchase"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult PurchaseReport()
        {
            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/
            // throw new NotImplementedException(@"
            //    Write a query to return an array of anonymous objects that have two properties. 

            //    1. A Product property containing that particular product
            //    2. A QuantityPurchased property containing the number of times that product was purchased.

            //    This array should be ordered by QuantityPurchased in descending order.
            //");from o in _db.Orders
            //from od in _db.Order_Details
            //from p in _db.Products
            //where (o.OrderID == od.OrderID && od.ProductID == p.ProductID)

            // Select Count(productID) From [OrderDetails] Group by ProductId // marry me you coffee god



            var newObject = _db.Products
                .Select(p => new
                {
                    Product = p,
                    QuantityPurchased = p.Order_Details.Sum(od => od.Quantity)
                }
        ).OrderByDescending(p => p.QuantityPurchased);

            return Ok(newObject);


        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}