using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Enums
{
    public enum DeliveryStatus
    {
        [Description("Pendente")]
        Pending = 1,
        [Description("Sendo separado")]
        SeparateProducts = 2,
        [Description("Saiu para a entrega")]
        OutForDelivery = 3,
        [Description("Entregue")]
        Delivered = 4,
        [Description("Devolvido")]
        Returned = 5
    }
}
