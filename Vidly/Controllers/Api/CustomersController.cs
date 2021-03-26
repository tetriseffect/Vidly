using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;



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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //GET /api/customers/1 (Single Customer. 1 is their ID)
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // POST /apli/customers (Creating a customer)
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            // First validate the object. 
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            // Map a customerddto, Add the new customer to the db, then save. 
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
        }


        // PUT /api/customers/1 (Updating a customer)
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
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

            // Removed '<CustomerDto, Customer>' after .Map, because the compiler can infer from the source and target types from the two objects passed to the method.
            Mapper.Map(customerDto, customerInDb);

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
