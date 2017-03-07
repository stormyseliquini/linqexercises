﻿using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class EmployeesController : ApiController
    {
        private NORTHWNDEntities _db;

        public EmployeesController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/employees
        [HttpGet, Route("api/employees"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployees()
        {
            //throw new NotImplementedException("Write a query to return all employees");
            //Select* From Employees
            return Ok(_db.Employees);
        }

        // GET: api/employees/title/Sales Manager
        [HttpGet, Route("api/employees/title/{title}"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployeesByTitle(string title)
        {
            //throw new NotImplementedException("Write a query to return all employees with the given Title");
            //Select* From Employees where Title=''

            var eTitle = _db.Employees
                        .Where(e => e.Title == title);
            return Ok(eTitle);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}