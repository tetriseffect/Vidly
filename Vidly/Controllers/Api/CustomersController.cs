using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;



namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers (List of Customers)
        public IHttpActionResult GetCustomers()
        {
            var customerDtos = _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET /api/customers/1 (Single Customer. 1 is their ID)
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers (Creating a customer)
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            // First validate the object. 
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            // Map a customerddto, Add the new customer to the db, then save. 
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }


        // PUT /api/customers/1 (Updating a customer)
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Get the customer in the db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // Check if id given by customer in invalid.
            if (customerInDb == null)
            {
                return NotFound();
            }

            // Removed '<CustomerDto, Customer>' after .Map, because the compiler can infer from the source and target types from the two objects passed to the method.
            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1 (Deleting a customer)
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            // Get the customer in the db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // Check if id given by customer in invalid
            if (customerInDb == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
