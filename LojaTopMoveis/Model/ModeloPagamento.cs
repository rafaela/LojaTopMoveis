namespace LojaTopMoveis.Model
{
    public class ModeloPagamento
    {
        public string OrderNumber { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ExpirationDate { get; set; }
        public string SoftDescriptor { get; set; }
        public string MaxNumberOfInstallments { get; set; }
        public ModeloPagamentoCompra Shipping { get; set; }
        public ModeloPagamentoRecorrente Recurrent { get; set; }

    }
}
