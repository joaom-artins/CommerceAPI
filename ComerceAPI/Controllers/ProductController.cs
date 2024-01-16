using ComerceAPI.Context;
using ComerceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ComerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public ProductController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _dbContext.Products.ToListAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var productId = await _dbContext.Products.FindAsync(id);
            if(productId == null) return NotFound();
            return Ok(productId);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if(product is null) return BadRequest();

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return Ok(product);
        }
        [HttpPut ("{id:int}")]
        public async Task<ActionResult<Product>> Put(int id,Product product)
        {
            if(product.Id!=id) return BadRequest();
            var productId = await _dbContext.Products.FindAsync(id);
            productId.Name=product.Name;
            productId.Description=product.Description;
            productId.Price=product.Price;
            productId.ImageUrl=product.ImageUrl;
            productId.Quantity=product.Quantity;
            productId.CategoryId=product.CategoryId;

            await _dbContext.SaveChangesAsync();
            return Ok(productId);
        }
        [HttpDelete]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var productId= await _dbContext.Products.FindAsync(id);
            if(productId is null) return BadRequest();
            _dbContext.Products.Remove(productId);
            await _dbContext.SaveChangesAsync();
            return NotFound();
        }
    }
}
