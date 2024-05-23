using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Travel_agency_Lyapynova.Meneger
{
    /// <summary>
    /// Логика взаимодействия для Decoration_vaucher1.xaml
    /// </summary>
    public partial class Decoration_vaucher1 : System.Windows.Controls.Page
    {
        ServiceAgreement service;
        List<Tour> tours = new List<Tour>();
        public Decoration_vaucher1(int contactId)
        {
            InitializeComponent();
            service = TravelAgentsPr21101LyapynovaContext.GetContext().ServiceAgreements.FirstOrDefault(s => s.ContractId == contactId);
            LoadCB();
        }
        private void LoadCB()
        {
            cb_country.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Countries.Select(c => c.Name).ToList();
            cb_city.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Cities.Select(p => p.Name).ToList();

          
            tours = TravelAgentsPr21101LyapynovaContext.GetContext().Tours.Include(t => t.Country).Include(t => t.City).ToList();
            LViewTour.ItemsSource = tours;
            
        }
        private void LViewTour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LViewTour.SelectedItem != null)
            {
                // Получить выбранный тур
                Tour selectedTour = (Tour)LViewTour.SelectedItem;

                // Формирование информации о путевке
                string message = $"Информация о путевке:\n";
                message += $"Название: {selectedTour.Name}\n";
                message += $"Описание: {selectedTour.Descriptions}\n";
                message += $"Длительность: {selectedTour.Duration}\n";
                message += $"Цена: {selectedTour.Cost}\n";
                message += $"Место путевки: {selectedTour.Country.Name}, {selectedTour.City.Name}\n";
                message += $"Поставщик: {selectedTour.Supplier.Name}\n";


                // Показать сообщение с информацией и кнопками подтверждения
                MessageBoxResult result = MessageBox.Show(message, "Подтвердите выбор", MessageBoxButton.YesNo, MessageBoxImage.Information);

                // Обработка результата нажатия кнопок
                if (result == MessageBoxResult.Yes)
                {
                    // Применение стиля (изменение цвета) к выбранному туру
                    Style selectedTourStyle = new Style(typeof(ListBoxItem));
                    selectedTourStyle.Setters.Add(new Setter(ListBoxItem.BackgroundProperty, Brushes.LightBlue));
                    ListBoxItem listBoxItem = (ListBoxItem)(LViewTour.ItemContainerGenerator.ContainerFromItem(selectedTour));
                    if (listBoxItem != null)
                    {
                        listBoxItem.Style = selectedTourStyle;
                    }
                }
                else
                {
                    // Код для отмены выбора путевки
                    MessageBox.Show("Выбор отменен", "Отмена", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void cb_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_country.SelectedItem != null)
            {
                string selectedCountry = cb_country.SelectedItem.ToString();

                var selectedCountryId = TravelAgentsPr21101LyapynovaContext.GetContext().Countries
                    .Where(c => c.Name == selectedCountry)
                    .Select(c => c.CountryId)
                    .FirstOrDefault();

                var citiesInCountry = TravelAgentsPr21101LyapynovaContext.GetContext().Cities
                    .Where(city => city.CountryId == selectedCountryId)
                    .Select(city => city.Name)
                    .ToList();

                cb_city.ItemsSource = citiesInCountry;
                tours = TravelAgentsPr21101LyapynovaContext.GetContext().Tours.Include(t => t.Country).Include(t => t.City).Where(t => t.Country == cb_country.SelectedItem).ToList();
                LViewTour.ItemsSource = tours;


            }
        }

        private void cb_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_country.SelectedItem == null)
            {
                MessageBox.Show("Выберите страну!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (cb_city.SelectedItem != null) { }
            {
                tours = TravelAgentsPr21101LyapynovaContext.GetContext().Tours.Include(t => t.Country).Include(t => t.City).Where(t => t.Country == cb_country.SelectedItem && t.City == cb_city.SelectedItem).ToList();
                LViewTour.ItemsSource = tours;
            }
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            if (LViewTour.SelectedItem != null)
            {
                Tour selectedTour = (Tour)LViewTour.SelectedItem;

                // Получение выбранного тура из базы данных
                Tour selectedTourFromDB = TravelAgentsPr21101LyapynovaContext.GetContext().Tours
                    .Include(t => t.Country)
                    .Include(t => t.City)
                    .FirstOrDefault(t => t.TourId == selectedTour.TourId);

                if (selectedTourFromDB != null)
                {
                    NavigationService.Navigate(new Decoration_vaucher2(selectedTourFromDB, service));
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить выбранный тур из базы данных. Пожалуйста, выберите тур снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите путевку для продолжения оформления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
 
