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
    /// Логика взаимодействия для MainMenegerPage.xaml
    /// </summary>
    public partial class MainMenegerPage : Page
    {
        public MainMenegerPage(Employee employee)
        {
            InitializeComponent();
        }

        private void btn_list_employee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenegerPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_travel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_document_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
