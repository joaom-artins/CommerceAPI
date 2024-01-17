using ComerceAPI.Context;
using ComerceAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public CartController(AppDBContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }

        [HttpGet]
        public async Task<IEnumerable<Cart>> Get()
        {
            return await _dbContext.Carts
         .Include(c => c.Client)
         .Include(c => c.Products)
         .ToListAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cart>> Get(int id)
        {
            var cartId=await _dbContext.Carts.FindAsync(id);
            if (cartId is null) return BadRequest();
            return Ok(cartId.Products);
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> Post(Cart cart)
        {
            if (cart is null) return BadRequest();
            await _dbContext.Carts.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return Ok(cart);
        }
        [HttpPut("{id:int}", Name = "AdicionarProduto")]
        public async Task<ActionResult<Cart>> PutAdd(int id, int productId)
        {
            var cartId = await _dbContext.Carts.FindAsync(id);
            if (cartId is null) return BadRequest();
            var product = await _dbContext.Products.FindAsync(productId);
            if (product is null) return BadRequest();
            cartId.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return Ok(cartId.Products);

        }
        [HttpPut("{id:int}&{productId:int}", Name = "RetirarProduto")]
        public async Task<ActionResult<Cart>> PutRemove(int id, int productId)
        {
            var cartId = await _dbContext.Carts.FindAsync(id);
            if (cartId is null) return BadRequest();
            var product = await _dbContext.Products.FindAsync(productId);
            if (product is null) return BadRequest();
            cartId.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok(cartId.Products);
        }
        [HttpPut]
        public async Task<ActionResult<Cart>> Put(int id)
        {
            var cartId = await _dbContext.Carts.FindAsync(id);
            if (cartId is null) return BadRequest();
            cartId.Products.RemoveAll(x=>x.Quantity>0);
            return NotFound();
        }
        [HttpDelete ("{id:int}")]
        public async Task<ActionResult<Cart>> Delete(int id)
        {
            var cartId = await _dbContext.Carts.FindAsync(id);
            if(cartId is null) return BadRequest(); 
            _dbContext.Carts.Remove(cartId);
            await _dbContext.SaveChangesAsync();
            return NotFound();
        }
    }
}
