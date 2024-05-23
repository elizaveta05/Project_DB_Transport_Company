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
using Travel_agency_Lyapynova.Meneger;
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova
{
    /// <summary>
    /// Логика взаимодействия для MenegerPage.xaml
    /// </summary>
    public partial class MenegerPage : Page
    {
        List<Klient> klient = new List<Klient>();
        public int employeeId;
        public MenegerPage(int employeeId)
        {
            InitializeComponent();
            LoadEmployee();
        }

       

        private void LoadEmployee()
        {
            klient=TravelAgentsPr21101LyapynovaContext.GetContext().Klients.ToList();
            klientListView.ItemsSource= klient;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddKlient());
        }

        private void btn_poisk_Click(object sender, RoutedEventArgs e)
        {
            string search = tb_poisk.Text.Trim().ToLower();
            List<Klient> searchResults = klient.Where(emp => emp.Name.ToLower().Contains(search) ||
                                                               emp.Surname.ToLower().Contains(search) ||
                                                               emp.Patronymic.ToLower().Contains(search)).ToList();

            klientListView.ItemsSource = searchResults;
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
                        klient = klient.OrderBy(emp => emp.Surname).ToList();
                        break;
                    case "По алфавиту(Я-А)":
                        klient = klient.OrderByDescending(emp => emp.Surname).ToList();
                        break;
                }

                klientListView.ItemsSource = klient;
            }
        }

        private void employeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Klient selectedKlient = klientListView.SelectedItem as Klient;

            if (selectedKlient != null)
            {
                NavigationService.Navigate(new ProfileKlient(selectedKlient.KlientId, employeeId));
                klientListView.SelectedItem = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
