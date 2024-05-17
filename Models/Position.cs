using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string NamePositions { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
