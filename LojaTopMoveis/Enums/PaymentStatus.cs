using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Enums
{
    public enum PaymentStatus
    {
        [Description("Pendente")]
        Pending = 1,
        [Description("Pago")]
        Paid = 2,
        [Description("Cancelado")]
        Canceled = 3
    }
}
