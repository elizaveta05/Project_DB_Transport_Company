using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Transport
{
    public int TransportId { get; set; }

    public int TypeTransportId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public string Company { get; set; } = null!;

    public decimal Cost { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual TypeOfTransport TypeTransport { get; set; } = null!;
}
