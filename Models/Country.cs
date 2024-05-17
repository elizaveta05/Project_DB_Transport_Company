using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Excursion> Excursions { get; set; } = new List<Excursion>();

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual ICollection<RegistrationOfInsurance> RegistrationOfInsurances { get; set; } = new List<RegistrationOfInsurance>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public virtual ICollection<VisaProcessing> VisaProcessings { get; set; } = new List<VisaProcessing>();
}
