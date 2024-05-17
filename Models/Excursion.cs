using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Excursion
{
    public int ExcursionId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Cost { get; set; }

    public string CompanyNumberPhone { get; set; } = null!;

    public int CountryId { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<ExcursionsOrder> ExcursionsOrders { get; set; } = new List<ExcursionsOrder>();
}
