using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class VisaProcessing
{
    public int VisaProcessId { get; set; }

    public int RegistationId { get; set; }

    public int CountryId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int TypeVisa { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual RegistrationOfDocument Registation { get; set; } = null!;

    public virtual TypeOfVisa TypeVisaNavigation { get; set; } = null!;
}
