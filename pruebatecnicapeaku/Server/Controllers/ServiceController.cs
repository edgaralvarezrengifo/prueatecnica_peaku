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
    }
}
