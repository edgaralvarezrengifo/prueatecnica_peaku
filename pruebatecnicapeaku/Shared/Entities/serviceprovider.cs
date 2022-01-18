using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pruebatecnicapeaku.Shared.Entities
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        [Required]
        public Int64 Nit { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Creationdate { get; set; }
    }
}
