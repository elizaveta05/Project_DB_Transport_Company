using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Travel_agency_Lyapynova.Director;
using Travel_agency_Lyapynova.Meneger;
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void btn_authorization_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_login.Text.Trim();
            string password = tb_password.Password.Trim();
            var user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
                Clear();
            }
            else
            {
                Role_definition(user);
            }
        }

        private void Role_definition(User user)
        {
            try
            {
                var employee = TravelAgentsPr21101LyapynovaContext.GetContext().Employees.FirstOrDefault(e => e.UserId == user.UserId);
                var supplier = TravelAgentsPr21101LyapynovaContext.GetContext().Suppliers.FirstOrDefault(s => s.UserId == user.UserId);

                if (employee != null)
                {
                    switch (employee.PositionId)
                    {
                        case 2:
                            NavigationService.Navigate(new MainMenegerPage(employee)); 
                            break;
                        case 3:
                            NavigationService.Navigate(new MainDirectorPage(employee));
                            break;
                        default:
                            MessageBox.Show("В данный момент для вашей должности недоступно использование приложения");
                            break;
                    }
                }
                else if (supplier != null)
                {
                    NavigationService.Navigate(new SupplierPage(supplier));
                }
                else
                {
                    MessageBox.Show("Сотрудник или поставщик не найден");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                Clear();
            }
        }

        private void Clear()
        {
            tb_login.Clear();
            tb_password.Clear();
        }
    }
}