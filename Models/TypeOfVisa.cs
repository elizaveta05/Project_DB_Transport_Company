using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class TypeOfVisa
{
    public int TypeVisaId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VisaProcessing> VisaProcessings { get; set; } = new List<VisaProcessing>();
}
