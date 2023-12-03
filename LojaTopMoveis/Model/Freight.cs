using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Model
{
    public class Freight
    {
        public Guid Id { get; set; }
        public decimal? ValueKm { get; set; }
        public string? ValuePriceFreeShipping { get; set; }
        public string? CityFreeShipping { get; set; }
        public int? TimeDeliveryDays { get; set; }
        public bool FreeShipping { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}
