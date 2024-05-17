using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ContractId { get; set; }

    public decimal Cost { get; set; }

    public int PaymentMethodId { get; set; }

    public DateOnly DateOfPayment { get; set; }

    public virtual ServiceAgreement Contract { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
