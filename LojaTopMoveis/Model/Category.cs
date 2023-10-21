using System;

namespace Loja.Model
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Inactive { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}
