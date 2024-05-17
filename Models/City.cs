using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class City
{
    public int CityId { get; set; }

    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Excursion> Excursions { get; set; } = new List<Excursion>();

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
