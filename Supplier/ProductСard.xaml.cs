using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Travel_agency_Lyapynova
{
    /// <summary>
    /// Логика взаимодействия для ProductСard.xaml
    /// </summary>
    public partial class ProductСard : Page
    {
        public Tour tour;
        public Supplier supplier;
        public ProductСard(int tourId, Supplier supplier)
        {
            InitializeComponent();
            tour = TravelAgentsPr21101LyapynovaContext.GetContext().Tours.FirstOrDefault(e => e.TourId == tourId);
            LoadTour(tour);
            this.supplier = supplier;
        }
        public void LoadTour(Tour tour)
        {
            try
            {
                tb_name.Text = tour.Name;
                tb_discription.Text = tour.Descriptions;
                tb_duration.Text = tour.Duration.ToString();
                tb_cost.Text = tour.Cost.ToString();
                cb_country.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Countries.Select(c => c.Name).ToList();
                cb_country.SelectedIndex = tour.CountryId - 1;
                cb_city.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Cities.Select(p => p.Name).ToList();
                cb_city.SelectedIndex=tour.CityId - 1;
                cb_supplier.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Suppliers.Select(p => p.Name).ToList();
                cb_supplier.SelectedIndex=tour.SupplierId - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (tour.SupplierId == supplier.SupplierId)
            {
                tour.Name = tb_name.Text;
                tour.Descriptions = tb_discription.Text;
                tour.Duration = int.Parse(tb_duration.Text);
                tour.Cost = decimal.Parse(tb_cost.Text);
                tour.CountryId = TravelAgentsPr21101LyapynovaContext.GetContext().Countries
                   .OrderBy(p => p.CountryId)
                   .Select(p => p.CountryId)
                   .ToList()[cb_country.SelectedIndex];
                tour.CityId = TravelAgentsPr21101LyapynovaContext.GetContext().Cities
               .OrderBy(p => p.CityId)
               .Select(p => p.CityId)
               .ToList()[cb_city.SelectedIndex];
                tour.CityId = TravelAgentsPr21101LyapynovaContext.GetContext().Suppliers
             .OrderBy(p => p.SupplierId)
             .Select(p => p.SupplierId)
             .ToList()[cb_supplier.SelectedIndex];
                var validationResults = tour.Validate(new ValidationContext(tour));
                if (validationResults.Any())
                {
                    string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                    MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                    return;
                }
                TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

                MessageBox.Show("Данные о туре успешно сохранены.", "Успех", MessageBoxButton.OK);
            } else
            {
                Close();
                MessageBox.Show("Вы не являетесь поставщиком этого тура, поэтому вы можете только просматривать его данные!", "Ошибка", MessageBoxButton.OK);
            }
            
        }
        private void Close()
        {
            tb_name.IsEnabled = false;
            tb_discription.IsEnabled = false;
            tb_duration.IsEnabled = false;
            tb_cost.IsEnabled = false;
            cb_city.IsEnabled = false;
            cb_supplier.IsEnabled = false;
            cb_country.IsEnabled= false;
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот тур?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (tour.SupplierId == supplier.SupplierId) 
            {
                TravelAgentsPr21101LyapynovaContext.GetContext().Tours.Remove(tour);
                TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

                MessageBox.Show("Тур успешно удален.", "Успех", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Вы не можете удалить этот тур, так как он принадлежит другому поставщику.", "Ошибка", MessageBoxButton.OK);
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
            }
        }
    }
}
