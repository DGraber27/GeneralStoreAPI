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
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        //Create Product (POST)
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct(Product model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            if (ModelState.IsValid)
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return Ok("Customer has been created and saved!");
            }
            return BadRequest(ModelState);
        }
        // Get All Products (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        //Get a product by its ID (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetProductById(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        //Update an existing product by its ID(PUT)
        [HttpPut]
        public async Task<IHttpActionResult> Updateprodutct([FromUri] int id, [FromBody] Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.Name = updatedProduct.Name;
                    product.Cost = updatedProduct.Cost;
                    product.Inventory = updatedProduct.Inventory;
                    await _context.SaveChangesAsync();
                    return Ok("product succesfully updated.");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //Delete an existing Product by its ID(DELETE)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomerByID(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("Product succesfully deleted.");
            }
            return InternalServerError();
        }
    }
}
