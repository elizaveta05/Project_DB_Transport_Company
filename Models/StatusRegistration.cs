using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class StatusRegistration
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<HistoryRegistration> HistoryRegistrations { get; set; } = new List<HistoryRegistration>();
}
