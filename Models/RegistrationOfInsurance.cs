using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class RegistrationOfInsurance
{
    public int RegistrationInsuranceId { get; set; }

    public int RegistationId { get; set; }

    public int TypeInsuranceId { get; set; }

    public int CountryId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual RegistrationOfDocument Registation { get; set; } = null!;

    public virtual TypeOfInsurance TypeInsurance { get; set; } = null!;
}
