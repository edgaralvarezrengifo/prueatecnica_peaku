using Microsoft.AspNetCore.Mvc;
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
    }
}