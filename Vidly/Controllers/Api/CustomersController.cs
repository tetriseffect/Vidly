using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

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
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //GET /api/customers/1 (Single Customer. 1 is their ID)
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }

        // POST /apli/customers (Creating a customer)
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            // First validate the object. 
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // Add the new customer to the db, then save
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }


        // PUT /api/customers/1 (Updating a customer)
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // Get the customer in the db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // Check if id given by customer in invalid
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customerInDb.Name = customer.Name;
            customerInDb.Birthday = customer.Birthday;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }

        // DELETE /api/customers/1 (Deleting a customer)
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            // Get the customer in the db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // Check if id given by customer in invalid
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
