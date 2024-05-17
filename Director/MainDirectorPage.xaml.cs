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
    /// Логика взаимодействия для MainDirectorPage.xaml
    /// </summary>
    public partial class MainDirectorPage : Page
    {

        public MainDirectorPage(Employee employee)
        {
            InitializeComponent();
            Welcome(employee);

        }
        private void Welcome(Employee employee)
        {
            tb_welcome.Text = "Добро пожаловать " + employee.Surname + " " + employee.Name + " " + employee.Patronymic + "!";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }

        private void btn_list_employee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirectorPage());
        }

        private void btn_list_supplier_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListSuppliersPage());
        }
    }
}
