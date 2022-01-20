using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebatecnicapeaku.Server.Models;
using pruebatecnicapeaku.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruebatecnicapeaku.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ServiceController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult<int>>Post(Service service)
        {
            context.Add(service);
            await context.SaveChangesAsync();
            return service.Id;

        }

        [HttpPut]
        public async Task<ActionResult> Put(Service service)
        {
            context.Attach(service).State=EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id)
        {
            var exists = await context.Service.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }
            else
            {
                context.Remove(new Service { Id = id });
                await context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Service>>> Get()
        {
            return await context.Service.ToListAsync();
        }

        [HttpGet("search/{providername}")]
        public async Task<ActionResult<int>> Get(string providername)
        {
           
            return await context.Service.Where(x => x.serviceprovider.Name == providername).CountAsync();
        }
    }
}
