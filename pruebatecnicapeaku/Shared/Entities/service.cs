using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pruebatecnicapeaku.Shared.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public int IdServiceProvider { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public DateTime Creationdate { get; set; }
        public ServiceProvider serviceprovider { get; set; }
    }
}
