using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Model
{
    public class Color
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey("ProductId")]
        public Guid? ProductId { get; set; }
        public string? urlImage { get; set; }
        public string? ImageBase64 { get; set; }
        public int Amount { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}
