using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class ExcursionsOrder
{
    public int ExcursionOrderId { get; set; }

    public int ExcursionId { get; set; }

    public int ContractId { get; set; }

    public DateOnly DateOrder { get; set; }

    public virtual ServiceAgreement Contract { get; set; } = null!;

    public virtual Excursion Excursion { get; set; } = null!;
}
