using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pruebatecnicapeaku.Shared.Entities;

namespace pruebatecnicapeaku.Client.Repositories
{
    public interface IRepository
    {
        List<ServiceProvider> GetServiceproviders();
        List<Service> GetServices();
    }
}
