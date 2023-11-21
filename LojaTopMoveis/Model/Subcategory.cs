using Loja.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaTopMoveis.Model
{
    public class Subcategory
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public bool Inactive { get; set; }
        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}
