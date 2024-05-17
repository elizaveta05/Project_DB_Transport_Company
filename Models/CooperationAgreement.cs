using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class CooperationAgreement
{
    public int ContractId { get; set; }

    public int SupplierId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
