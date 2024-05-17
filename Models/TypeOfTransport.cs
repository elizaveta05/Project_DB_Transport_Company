using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class TypeOfTransport
{
    public int TypeTransportId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
