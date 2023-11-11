using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Inactive { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();


    }
}
