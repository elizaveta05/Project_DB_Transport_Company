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

namespace Travel_agency_Lyapynova
{
    /// <summary>
    /// Логика взаимодействия для SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : Page
    {
        List<Tour> tours = new List<Tour>();
        public Supplier supplier;

        public SupplierPage(Supplier supplier)
        {
            InitializeComponent();
            this.supplier = supplier;
            Welcome(supplier);
            LoadTours();
        }

        private void Welcome(Supplier supplier)
        {
            tb_welcome.Text = "Добро пожаловать " + supplier.Name + "!";
        }

        private void LoadTours()
        {
            using (var context = new TravelAgentsPr21101LyapynovaContext())
            {
                tours = context.Tours.Include(t => t.Country).Include(t => t.City).ToList();

                LViewTour.ItemsSource = tours;
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddTours(supplier));

        }

        private void btn_poisk_Click(object sender, RoutedEventArgs e)
        {
            string search = tb_poisk.Text.Trim().ToLower();
            List<Tour> searchResults = tours.Where(emp => emp.Name.ToLower().Contains(search)).ToList();

            LViewTour.ItemsSource = searchResults;
        }

        private void cb_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cb_filter.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string selectedOption = selectedItem.Content.ToString();

                if (selectedOption == "Мои туры")
                {
                    tours = TravelAgentsPr21101LyapynovaContext.GetContext().Tours.Include(t => t.Country).Include(t => t.City).Where(t => t.SupplierId == supplier.SupplierId).ToList();
                }
                else if (selectedOption == "Все туры")
                {
                    LoadTours();
                }

                LViewTour.ItemsSource = tours;
            }
        }
      
        private void LViewTour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tour selectedTour = LViewTour.SelectedItem as Tour;

            if (selectedTour != null)
            {
                NavigationService.Navigate(new ProductСard(selectedTour.TourId, supplier));
                LViewTour.SelectedItem = null;
            }

        }
    }
}
