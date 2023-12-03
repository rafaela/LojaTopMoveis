using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Model
{
    public class Address
    {
        public Guid Id { get; set; }
        public string? Street { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? CEP { get; set; }
        public string? State { get; set; }
        public string? Reference { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public Client? Client { get; set; }
        [ForeignKey("ClientId")]
        public Guid? ClientId { get; set; }
        public bool Inactive { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}
