using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class HistoryRegistration
{
    public int HistoryId { get; set; }

    public int RegistrationId { get; set; }

    public int StatusId { get; set; }

    public DateOnly Date { get; set; }

    public virtual RegistrationOfDocument Registration { get; set; } = null!;

    public virtual StatusRegistration Status { get; set; } = null!;
}
