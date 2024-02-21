using Topmoveis.Enums;

namespace LojaTopMoveis.Model
{
    public class Payment
    {
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        public PaymentMethods? Methods { get; set; }
        public int? QuantityInstallments { get; set; }
        public bool? Inactive { get; set; }
        public double? Fees { get; set; }
        public double? Discount { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();



    }
}
