using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Stars { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public decimal Cost { get; set; }

    public int CountryId { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
