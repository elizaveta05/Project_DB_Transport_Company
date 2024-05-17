using System;
using System.Collections.Generic;

namespace Travel_agency_Lyapynova.Models;

public partial class RegistrationOfDocument
{
    public int RegistrationId { get; set; }

    public int ContractId { get; set; }

    public DateOnly DateOfApplication { get; set; }

    public DateOnly DateOfReceipt { get; set; }

    public virtual ServiceAgreement Contract { get; set; } = null!;

    public virtual ICollection<HistoryRegistration> HistoryRegistrations { get; set; } = new List<HistoryRegistration>();

    public virtual ICollection<RegistrationOfInsurance> RegistrationOfInsurances { get; set; } = new List<RegistrationOfInsurance>();

    public virtual ICollection<VisaProcessing> VisaProcessings { get; set; } = new List<VisaProcessing>();
}
