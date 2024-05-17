using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Transfer
{
    public int TransferId { get; set; }

    public int TypeTransportId { get; set; }

    public decimal Cost { get; set; }

    public string Company { get; set; } = null!;

    public string CompanyNumberPhone { get; set; } = null!;

    public virtual ICollection<TransferOrder> TransferOrders { get; set; } = new List<TransferOrder>();

    public virtual TypeOfTransport TypeTransport { get; set; } = null!;
}
