using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pruebatecnicapeaku.Shared.Entities;


namespace pruebatecnicapeaku.Client.Repositories
{
    public class Repository : IRepository
    {

        public List<ServiceProvider> GetServiceproviders()
        {
            return new List<ServiceProvider>() {
                new ServiceProvider(){Id=1,Email="test1@gmail.com",Name="Test1",Nit=890312771,Creationdate=DateTime.Now},
                new ServiceProvider(){Id=2,Email="test2@gmail.com",Name="Test2",Nit=890312772,Creationdate=DateTime.Now},
                new ServiceProvider(){Id=3,Email="test3@gmail.com",Name="Test3",Nit=890312773,Creationdate=DateTime.Now},
                new ServiceProvider(){Id=4,Email="test4@gmail.com",Name="Test4",Nit=890312774,Creationdate=DateTime.Now}
            };
        }
        public List<Service> GetServices()
        {
            return new List<Service>()
            {
                new Service(){Id=1,IdServiceProvider=1,Name="Service test1",Country="Colombia",price=2000}
            };
        }



    }
}
