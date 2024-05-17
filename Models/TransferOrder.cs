using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class TransferOrder
{
    public int TransferOrderId { get; set; }

    public int TransferId { get; set; }

    public int ContractId { get; set; }

    public DateOnly DateOrder { get; set; }

    public virtual ServiceAgreement Contract { get; set; } = null!;

    public virtual Transfer Transfer { get; set; } = null!;
}
