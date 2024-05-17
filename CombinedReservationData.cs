using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova
{
    public class CombinedReservationData
    {
        public int ReservationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int ContractId { get; set; }

        public int ServiceAgreementId { get; set; }
        public int KlientId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly DateOfConclusion { get; set; }
        public string Conditions { get; set; }


        public CombinedReservationData(Reservation reservation, ServiceAgreement serviceAgreement)
        {
            ReservationId = reservation.ReservationId;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
            ContractId = reservation.ContractId;

            ServiceAgreementId = serviceAgreement.ContractId; 
            KlientId = serviceAgreement.KlientId;
            EmployeeId = serviceAgreement.EmployeeId;
            DateOfConclusion = serviceAgreement.DateOfConclusion;
            Conditions = serviceAgreement.Conditions;

        }
    }
}
