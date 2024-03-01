using LojaTopMoveis.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Topmoveis.Enums;

namespace Topmoveis.Model
{
    public class Sale
    {
        public Guid Id { get; set; }

        [ForeignKey("ClientId")]
        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }
        public List<ProductsSale>? ProductsSale { get; set; }
        public List<Product>? Products { get; set; }
        public string? Name { get; set; }
        public decimal? ValorTotal { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public DateTime DateSale { get; set; }
        public DateTime DateDelivery { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();

    }
}
