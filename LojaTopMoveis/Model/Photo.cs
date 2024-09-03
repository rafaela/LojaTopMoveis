using System.ComponentModel.DataAnnotations.Schema;
using Topmoveis.Model;

namespace LojaTopMoveis.Model
{
    public class Photo
    {

        public Guid ID  { get; set; }
        public string? Description { get; set; }    
        public string? urlImage { get; set; }
        [ForeignKey("ProductId")]
        public Guid? ProductId { get; set; }
        public bool Inactive { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
        public byte[]? Imagem { get; set; }
    }
}
