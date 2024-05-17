using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class History
{
    public int HistoryId { get; set; }

    public int KlientId { get; set; }

    public int СontractId { get; set; }

    public virtual Klient Klient { get; set; } = null!;

    public virtual ServiceAgreement Сontract { get; set; } = null!;
}
