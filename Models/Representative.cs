using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Representative
{
    public int RepresentativeId { get; set; }

    public int TourId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
