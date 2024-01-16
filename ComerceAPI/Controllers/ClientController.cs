using ComerceAPI.Context;
using ComerceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController:ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public ClientController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _dbContext.Clients.ToListAsync();
        }
        [HttpGet ("{id:int}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var clientId=await _dbContext.Clients.FindAsync(id);
            if(clientId is null)
            {
                return NotFound();
            }
            return Ok(clientId);
        }
        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client client)
        {
            if (client is null) return BadRequest();
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
            return Ok(client);
        }
        [HttpPut ("{id:int}")]
        public async Task<ActionResult<Client>> Put(int id,Client client)
        {
            if(client.Id!=id) return BadRequest();
            var clientId = await _dbContext.Clients.FindAsync(id);
            clientId.Name= client.Name;
            clientId.CPF= client.CPF;
            await _dbContext.SaveChangesAsync();
            return Ok(clientId);
        }
        [HttpDelete ("{id:int}")]
        public async Task<ActionResult<Client>> Delete(int id)
        {
            var clientId = await _dbContext.Clients.FindAsync(id);
            if (clientId is null)
            {
                return BadRequest();
            }
            _dbContext.Clients.Remove(clientId);
            await _dbContext.SaveChangesAsync();
            return NotFound();
        }
    }
}
