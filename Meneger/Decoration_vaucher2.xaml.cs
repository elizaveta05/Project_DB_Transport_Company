using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для Decoration_vaucher2.xaml
    /// </summary>
    public partial class Decoration_vaucher2 : Page
    {
        private Tour tour;
        private ServiceAgreement service;
        private List<Hotel> hotels = new List<Hotel>();
        private List<Transport> transports = new List<Transport>();

        public Decoration_vaucher2(Tour selectedTour, ServiceAgreement selectedService)
        {
            InitializeComponent();
            service = selectedService;
            tour = selectedTour;
            Load();
        }

        private void Load()
        {
            if (service != null && tour != null)
            {
                cb_type_transport.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().TypeOfTransports.Select(c => c.Name).ToList();

                int countryId = tour.CountryId;
                int cityId = tour.CityId;

                hotels = TravelAgentsPr21101LyapynovaContext.GetContext().Hotels.Include(h => h.Country).Include(h => h.City).Where(h => h.CountryId == countryId && h.CityId == cityId).ToList();
                LViewHotel.ItemsSource = hotels;

                transports = TravelAgentsPr21101LyapynovaContext.GetContext().Transports.Include(h => h.TypeTransport).ToList();
                LViewHotel.ItemsSource = hotels;

            }
        }
        private void cb_type_transport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_type_transport.SelectedItem != null)
            {
                string selectedTypeTransport = cb_type_transport.SelectedItem.ToString();

                TypeOfTransport typeTransport = TravelAgentsPr21101LyapynovaContext.GetContext().TypeOfTransports.FirstOrDefault(t => t.Name == selectedTypeTransport);

                if (typeTransport != null)
                {
                    transports = TravelAgentsPr21101LyapynovaContext.GetContext().Transports.Include(h => h.TypeTransport)
                                                                                      .Where(h => h.TypeTransportId == typeTransport.TypeTransportId)
                                                                                      .ToList();
                    LViewTransport.ItemsSource = transports;
                }
                else
                {
                    MessageBox.Show("Выбранный тип транспорта не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            if (LViewHotel.SelectedItem != null && LViewTransport.SelectedItem != null)
            {
                Hotel selectedHotel = (Hotel)LViewHotel.SelectedItem;
                Transport selectedTransport = (Transport)LViewTransport.SelectedItem;

                Window dateSelectionWindow = new Window
                {
                    Title = "Выбор дат",
                    Width = 300,
                    Height = 200,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                DatePicker startDatePicker = new DatePicker();
                startDatePicker.SelectedDate = DateTime.Today;

                DatePicker endDatePicker = new DatePicker();
                endDatePicker.SelectedDate = DateTime.Today;

                Button confirmButton = new Button
                {
                    Content = "Подтвердить",
                    Margin = new Thickness(0, 10, 0, 0)
                };

                confirmButton.Click += (s, ev) =>
                {
                    dateSelectionWindow.Close();

                    if (startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null)
                    {
                        MessageBox.Show("Выберите даты начала и конца отпуска", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    DateOnly startDate = DateOnly.FromDateTime(startDatePicker.SelectedDate.Value);
                    DateOnly endDate = DateOnly.FromDateTime(endDatePicker.SelectedDate.Value);

                    if (endDate < startDate)
                    {
                        MessageBox.Show("Дата окончания отпуска не может быть раньше даты начала", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (endDate > DateOnly.FromDateTime(DateTime.Today) || startDate > DateOnly.FromDateTime(DateTime.Today))
                    {
                        MessageBox.Show("Выбранные даты не могут быть позже сегодняшнего дня", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    int maxid = TravelAgentsPr21101LyapynovaContext.GetContext().Reservations.Max(em => em.ReservationId);

                    // Создание новой резервации
                    Reservation newReservation = new Reservation
                    {
                        ReservationId = maxid + 1,
                        StartDate = startDate,
                        EndDate = endDate,
                        ContractId = service.ContractId,
                        TourId = tour.TourId,
                        HotelId = selectedHotel.HotelId,
                        TransportId = selectedTransport.TransportId,
                        DateReservation = DateOnly.FromDateTime(DateTime.Today)
                    };

                    TravelAgentsPr21101LyapynovaContext.GetContext().Reservations.Add(newReservation);
                    TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

                    MessageBox.Show("Резервация успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                };
                StackPanel panel = new StackPanel();
                panel.Children.Add(new Label { Content = "Выберите дату начала отпуска:" });
                panel.Children.Add(startDatePicker);
                panel.Children.Add(new Label { Content = "Выберите дату конца отпуска:" });
                panel.Children.Add(endDatePicker);
                panel.Children.Add(confirmButton);

                dateSelectionWindow.Content = panel;

                dateSelectionWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите отель и транспорт!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LViewHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LViewHotel.SelectedItem != null)
            {
                Hotel selectedHotel = (Hotel)LViewHotel.SelectedItem;

                // Формирование информации о отеле
                string message = $"Информация о отеле:\n";
                message += $"Название: {selectedHotel.Name}\n";
                message += $"Адрес: {selectedHotel.Address}\n";
                message += $"Количество звезд: {selectedHotel.Stars}\n";
                message += $"Цена: {selectedHotel.Cost}\n";
                message += $"Место путевки: {selectedHotel.Country.Name}, {selectedHotel.City.Name}\n";
                message += $"Телефон: {selectedHotel.PhoneNumber}\n";


                // Показать сообщение с информацией и кнопками подтверждения
                MessageBoxResult result = MessageBox.Show(message, "Подтвердите выбор", MessageBoxButton.YesNo, MessageBoxImage.Information);

                // Обработка результата нажатия кнопок
                if (result == MessageBoxResult.Yes)
                {
                    // Применение стиля (изменение цвета) к выбранному отелю
                    Style selectedTourStyle = new Style(typeof(ListBoxItem));
                    selectedTourStyle.Setters.Add(new Setter(ListBoxItem.BackgroundProperty, Brushes.LightBlue));
                    ListBoxItem listBoxItem = (ListBoxItem)(LViewHotel.ItemContainerGenerator.ContainerFromItem(selectedHotel));
                    if (listBoxItem != null)
                    {
                        listBoxItem.Style = selectedTourStyle;
                    }
                }
                else
                {
                    // Код для отмены выбора отеля
                    MessageBox.Show("Выбор отменен", "Отмена", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void LViewTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LViewTransport.SelectedItem != null) 
                {
                Transport selectedTransport = (Transport)LViewTransport.SelectedItem;

                // Формирование информации о транспорте
                string message = $"Информация о транспорте:\n";
                message += $"Тип транспорта: {selectedTransport.TypeTransport.Name}\n";
                message += $"Номер рейса: {selectedTransport.FlightNumber}\n";
                message += $"Компания: {selectedTransport.Company}\n";
                message += $"Цена: {selectedTransport.Cost} Рублей\n";

                // Показать сообщение с информацией и кнопками подтверждения
                MessageBoxResult result = MessageBox.Show(message, "Подтвердите выбор", MessageBoxButton.YesNo, MessageBoxImage.Information);

                // Обработка результата нажатия кнопок
                if (result == MessageBoxResult.Yes)
                {
                    // Применение стиля (изменение цвета) к выбранному транспорту
                    Style selectedTransportStyle = new Style(typeof(ListBoxItem));
                    selectedTransportStyle.Setters.Add(new Setter(ListBoxItem.BackgroundProperty, Brushes.LightBlue));
                    ListBoxItem listBoxItem = (ListBoxItem)(LViewTransport.ItemContainerGenerator.ContainerFromItem(selectedTransport));
                    if (listBoxItem != null)
                    {
                        listBoxItem.Style = selectedTransportStyle;
                    }
                }
                else
                {
                    // Код для отмены выбора транспорта
                    MessageBox.Show("Выбор транспорта отменен", "Отмена", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    
    }
}