using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class ServiceAgreement
{
    public int ContractId { get; set; }

    public int KlientId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly DateOfConclusion { get; set; }

    public string Conditions { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<ExcursionsOrder> ExcursionsOrders { get; set; } = new List<ExcursionsOrder>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Klient Klient { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<RegistrationOfDocument> RegistrationOfDocuments { get; set; } = new List<RegistrationOfDocument>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<TransferOrder> TransferOrders { get; set; } = new List<TransferOrder>();
}
