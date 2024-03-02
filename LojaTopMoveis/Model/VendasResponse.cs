
using LojaTopMoveis.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Topmoveis.Enums;

namespace Topmoveis.Model
{
    public class VendasResponse
    {
        public Guid Id { get; set; }

        [ForeignKey("ClientId")]
        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }
        public List<Product>? Products { get; set; }
        public string? Name { get; set; }
        public decimal? ValorTotal { get; set; }
        public string? PaymentMethod { get; set; }
        public string? DateSale { get; set; }
        public string? DateDelivery { get; set; }
        public string? PaymentStatus { get; set; }
        public PaymentStatus? EPaymentStatus { get; set; }
        public DeliveryStatus? EDeliveryStatus { get; set; }

        public string? DeliveryStatus { get; set; }
        public string? CreationDate { get; set; }
        public string? ChangeDate { get; set; }

    }
}
