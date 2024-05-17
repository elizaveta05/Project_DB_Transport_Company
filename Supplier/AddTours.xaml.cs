using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
    /// Логика взаимодействия для AddTours.xaml
    /// </summary>
    public partial class AddTours : Page
    {
        public Supplier supplier;
        public AddTours(Supplier supplier)
        {
            InitializeComponent();
            this.supplier = supplier;
            UploadData(supplier);
        }
        private void UploadData(Supplier supplier)
        {
            cb_country.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Countries.Select(c => c.Name).ToList();
            cb_city.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Cities.Select(p => p.Name).ToList();
            tb_supplier.Text = supplier.Name;
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tour tour = new Tour();
                tour.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tb_name.Text.ToLower());
                tour.Descriptions = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tb_discription.Text.ToLower());
                tour.Duration = int.Parse(tb_duration.Text);
                tour.Cost = decimal.Parse(tb_cost.Text);
                tour.SupplierId = supplier.SupplierId;

                tour.CountryId = TravelAgentsPr21101LyapynovaContext.GetContext().Countries
               .OrderBy(p => p.CountryId)
               .Select(p => p.CountryId)
               .ToList()[cb_country.SelectedIndex];

                tour.CityId = TravelAgentsPr21101LyapynovaContext.GetContext().Cities
               .OrderBy(p => p.CityId)
               .Select(p => p.CityId)
               .ToList()[cb_city.SelectedIndex];

                var validationResults = tour.Validate(new ValidationContext(tour));
                if (validationResults.Any())
                {
                    string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                    MessageBox.Show("Ошибка при сохранении данных о туре: " + errorMessage);
                    return;
                }

                TravelAgentsPr21101LyapynovaContext.GetContext().Tours.Add(tour);
                TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();
                MessageBox.Show("Тур успешно добавлен.", "Успех", MessageBoxButton.OK);
            }catch(Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK);
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
