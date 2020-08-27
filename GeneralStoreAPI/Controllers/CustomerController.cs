using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        //Create a Customer (POST)
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer(Customer model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            if (ModelState.IsValid)
            {
                _context.Customers.Add(model);
                await _context.SaveChangesAsync();
                return Ok("Customer has been created and saved!");
            }
            return BadRequest(ModelState);
        }
        //Get All Customers (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }
        //Get Customer by ID (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetCusotmerById(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        //Update existing Customer by ID (PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            if (ModelState.IsValid)
            {
                Customer customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //Delete an existing Customer by its ID (Delete)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomerByID(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
                _context.Customers.Remove(customer);
            if (await _context.SaveChangesAsync()==1)
            {
                return Ok("Customer succesfully deleted.");
            }
            return InternalServerError();
        }
    }
}
