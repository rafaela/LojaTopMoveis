using Loja.Model;
using System.ComponentModel.DataAnnotations.Schema;
using Topmoveis.Model;

namespace LojaTopMoveis.Model
{
    public class ProductsSale
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey("SaleId")]
        public Guid? SaleId { get; set; }
        
        [ForeignKey("ProductId")]
        public Guid? ProductId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}
