using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int ContractId { get; set; }

    public int TourId { get; set; }

    public int HotelId { get; set; }

    public int TransportId { get; set; }

    public DateOnly DateReservation { get; set; }

    public virtual ServiceAgreement Contract { get; set; } = null!;

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;

    public virtual Transport Transport { get; set; } = null!;
}
