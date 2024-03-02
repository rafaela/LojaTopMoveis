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
        [Description("Aprovado")]
        RequestApproved = 2,
        [Description("Sendo separado")]
        SeparateProducts = 3,
        [Description("Saiu para a entrega")]
        OutForDelivery = 4,
        [Description("Entregue")]
        Delivered = 5,
        [Description("Devolvido")]
        Returned = 6
    }
}
