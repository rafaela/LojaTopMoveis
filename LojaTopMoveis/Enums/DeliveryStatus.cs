using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topmoveis.Enums
{
    public enum DeliveryStatus
    {
        RequestApproved = 1,
        SeparateProducts = 2,
        OutForDelivery = 3,
        Delivered = 4,
        Returned = 5
    }
}
