using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Enums
{
    public enum PaymentMethods
    {
        [Description("Crédito")]
        Credit = 1,
        [Description("Débito")]
        Debit = 2,
        [Description("Boleto")]
        Ticket = 3,
        [Description("Pix")]
        Pix = 4,
        [Description("Crédito Parcelado")]
        InstallmentCredit = 5
    }
}
