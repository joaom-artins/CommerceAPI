using ComerceAPI.Context;
using ComerceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public CategoryController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        [HttpGet ("id:int")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var categoryId= await _dbContext.Categories.FindAsync(id);
            if(categoryId is null) {
                return  NotFound();
            }
            return Ok(categoryId);
        }
        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category) 
        {
            if(category == null)
            {
                return BadRequest();
            }
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return Ok(category);
        }
        [HttpPut ("id:int")]
        public async Task<ActionResult<Category>> Put(int id,Category category)
        {
            
            if (category.Id != id)
            {
                return BadRequest();
            }
            var categoryId = await _dbContext.Categories.FindAsync(id);
            categoryId.Name =category.Name;
            await _dbContext.SaveChangesAsync();
            return Ok(categoryId);
        }
        [HttpDelete]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var categoryId = await _dbContext.Categories.FindAsync(id);
            if(categoryId is null)
            {
                return BadRequest();
            }
             _dbContext.Categories.Remove(categoryId);
            await _dbContext.SaveChangesAsync();
            return NotFound();
        }
    }
}
