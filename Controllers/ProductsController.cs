using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend1_2.Data;
using Backend1_2.Entities;
using Backend1_2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dbfirst_ehandel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductsController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProduct>>> GetProducts()
        {
            var products = new List<GetProduct>();

            foreach (var product in await _context.Products.ToListAsync())
                products.Add(new GetProduct
                {
                    Id = product.Id,
                    Image = product.Image,
                    Name = product.Name,
                    ShortDescription = product.ShortDescription,
                    LongDescription = product.LongDescription,
                    
                    Price = product.Price,
                    InStock = product.InStock

                });
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProduct model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Image = model.Image;
            product.Name = model.Name;
            product.ShortDescription = model.ShortDescription;
            product.LongDescription = model.LongDescription;
            product.Price = model.Price;
            product.InStock = model.InStock;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }





        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProduct model)
        {
            var product = new Product()
            {
                Image = model.Image,
                Name = model.Name,
                LongDescription = model.LongDescription,
                ShortDescription = model.ShortDescription,
                Price = model.Price,
                InStock = model.InStock
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }




        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
