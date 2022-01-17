using System;
using System.Collections.Generic;
using System.Text;

namespace pruebatecnicapeaku.Shared.Entities
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        public Int64 Nit { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Creationdate { get; set; }
    }
}
