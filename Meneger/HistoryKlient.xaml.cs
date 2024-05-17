using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova.Meneger
{
    /// <summary>
    /// Логика взаимодействия для HistoryKlient.xaml
    /// </summary>
    public partial class HistoryKlient : Page
    {
        public HistoryKlient(Klient klient)
        {
            InitializeComponent();
            if (klient != null)
            {
                LoadDate(klient);
                lbl.Content = "История клиента: " + klient.Surname + " " + klient.Name + " " + klient.Patronymic;
            }
        }
        public class ReservationData
        {
            public DateTime DateOfReservation { get; set; }
            public string HotelName { get; set; }
            public string TourName { get; set; }
            public string TransportCompany { get; set; }
        }

        private void LoadDate(Klient klient)
        {
            if (klient != null)
            {
                var serviceAgreements = TravelAgentsPr21101LyapynovaContext.GetContext().ServiceAgreements
                                        .Where(sa => sa.KlientId == klient.KlientId)
                                        .ToList();

                var reservationDataList = new List<ReservationData>();

                foreach (var sa in serviceAgreements)
                {
                    var saReservations = TravelAgentsPr21101LyapynovaContext.GetContext().Reservations
                                          .Where(r => r.ContractId == sa.ContractId)
                                          .ToList();

                    foreach (var reservation in saReservations)
                    {
                        var hotel = TravelAgentsPr21101LyapynovaContext.GetContext().Hotels
                                    .FirstOrDefault(h => h.HotelId == reservation.HotelId);

                        var tour = TravelAgentsPr21101LyapynovaContext.GetContext().Tours
                                   .FirstOrDefault(t => t.TourId == reservation.TourId);

                        var transport = TravelAgentsPr21101LyapynovaContext.GetContext().Transports
                                       .FirstOrDefault(tt => tt.TransportId == reservation.TransportId);

                        if (hotel != null && tour != null && transport != null)
                        {
                            DateTime convertedDate = DateTime.Parse(sa.DateOfConclusion.ToString()); 

                            reservationDataList.Add(new ReservationData
                            {
                                DateOfReservation = convertedDate,
                                HotelName = hotel.Name,
                                TourName = tour.Name,
                                TransportCompany = transport.Company
                            });
                        }
                    }
                }

                historyListView.ItemsSource = reservationDataList;
            }
        }
        private void employeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
}
