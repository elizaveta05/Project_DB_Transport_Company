using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class TypeOfInsurance
{
    public int TypeInsuranceId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RegistrationOfInsurance> RegistrationOfInsurances { get; set; } = new List<RegistrationOfInsurance>();
}
