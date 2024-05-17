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

namespace Travel_agency_Lyapynova.Director
{
    /// <summary>
    /// Логика взаимодействия для ListSuppliersPage.xaml
    /// </summary>
    public partial class ListSuppliersPage : Page
    {
        List<Supplier> suppliers = new List<Supplier>();
        public ListSuppliersPage()
        {
            InitializeComponent();
            LoadDate();
 
        }
        public void LoadDate()
        {
            using (var context = new TravelAgentsPr21101LyapynovaContext())
            {
                suppliers = context.Suppliers.ToList();
                supplierListView.ItemsSource = suppliers;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Director.MainDirectorPage());
        }

        private void btn_poisk_Click(object sender, RoutedEventArgs e)
        {
            string searchKeyword = tb_poisk.Text.Trim().ToLower();
            List<Supplier> searchResults = suppliers.Where(supplier =>
                supplier.Name.ToLower().Contains(searchKeyword)).ToList();

            supplierListView.ItemsSource = searchResults;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSupplierPage());

        }

        private void cb_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cb_filter.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string selectedOption = selectedItem.Content.ToString();

                switch (selectedOption)
                {
                    case "По алфавиту(А-Я)":
                        suppliers = suppliers.OrderBy(supplier => supplier.Name).ToList();
                        break;
                    case "По алфавиту(Я-А)":
                        suppliers = suppliers.OrderByDescending(supplier => supplier.Name).ToList();
                        break;
                }

                supplierListView.ItemsSource = suppliers;
            }
        }

        private void employeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (supplierListView.SelectedItem is Supplier selectedSupplier)
            {
                NavigationService?.Navigate(new ProfileSupplierPage(selectedSupplier.SupplierId));
            }
        }
    }
}
