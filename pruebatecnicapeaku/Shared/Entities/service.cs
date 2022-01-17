using System;
using System.Collections.Generic;
using System.Text;

namespace pruebatecnicapeaku.Shared.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public int IdServiceProvider { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public decimal price { get; set; }
        public DateTime Creationdate { get; set; }
    }
}
