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
using Travel_agency_Lyapynova.Director;
using Travel_agency_Lyapynova.Models;
using Microsoft.EntityFrameworkCore;

namespace Travel_agency_Lyapynova.Director
{  /// <summary>
   /// Логика взаимодействия для DirectorPage.xaml
   /// </summary>
    public partial class DirectorPage : Page
    {
        private List<Employee> employees = new List<Employee>();

        public DirectorPage()
        {
            InitializeComponent();
            LoadEmployee();
        }

        private void LoadEmployee()
        {
            using (var context = new TravelAgentsPr21101LyapynovaContext())
            {
                employees = context.Employees.Include(e => e.Position).ToList();
                UpdateEmployeeList();
            }
        }

        private void UpdateEmployeeList()
        {
            employeeListView.ItemsSource = employees;
        }

        private void ApplyFilter(string filterName)
        {
            switch (filterName)
            {
                case "По алфавиту(А-Я)":
                    employees = employees.OrderBy(emp => emp.Surname).ToList();
                    break;
                case "По алфавиту(Я-А)":
                    employees = employees.OrderByDescending(emp => emp.Surname).ToList();
                    break;
                default:
                    employees = employees.Where(emp => emp.Position.NamePositions == filterName).ToList();
                    break;
            }
            UpdateEmployeeList();
        }

        private void cb_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cb_filter.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                ApplyFilter(selectedItem.Content.ToString());
            }
        }

        private void btn_poisk_Click(object sender, RoutedEventArgs e)
        {
            string search = tb_poisk.Text.Trim().ToLower();
            List<Employee> searchResults = employees.Where(emp => emp.Name.ToLower().Contains(search) ||
                                                               emp.Surname.ToLower().Contains(search) ||
                                                               emp.Patronymic.ToLower().Contains(search)).ToList();

            employeeListView.ItemsSource = searchResults;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmplyee());
        }

        private void employeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = employeeListView.SelectedItem as Employee;

            if (selectedEmployee != null)
            {
                NavigationService.Navigate(new ProfileEmployee(selectedEmployee.EmployeeId));
                employeeListView.SelectedItem = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //.Navigate(new Director.MainDirectorPage(employee));
        }
    }
}
