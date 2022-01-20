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
    public class ServiceProviderController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ServiceProviderController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(ServiceProvider serviceProvider)
        {
            context.Add(serviceProvider);
            await context.SaveChangesAsync();
            return serviceProvider.Id;

        }
        [HttpPut]
        public async Task<ActionResult<int>>Put(ServiceProvider serviceProvider)
        {
            context.Attach(serviceProvider).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.ServiceProviders.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }
            else
            {
                context.Remove(new ServiceProvider { Id = id });
                await context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceProvider>>> Get(){
            return await context.ServiceProviders.ToListAsync();
        }

        [HttpGet("search/{day}")]
        public async Task<ActionResult<int>> Get(string day)
        {
            DateTime searchday = Convert.ToDateTime(day);
            return await context.ServiceProviders.Where(x => x.Creationdate == searchday).CountAsync();
        }
    }
}