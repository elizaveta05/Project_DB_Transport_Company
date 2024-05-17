using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Travel_agency_Lyapynova.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Travel_agency_Lyapynova
{
    public partial class AddEmplyee : Page
    {
        public AddEmplyee()
        {
            InitializeComponent();
            LoadPositions();
        }

        private void LoadPositions()
        {
            cb_position.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Positions.Select(p => p.NamePositions).ToList();
        }

        private void btn_save_employee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee employee = new Employee
                {
                    Surname = tb_surname.Text,
                    Name = tb_name.Text,
                    PhoneNumber = tb_number_phone.Text,
                    Patronymic = tb_patronymic.Text,
                    PositionId = TravelAgentsPr21101LyapynovaContext.GetContext().Positions.OrderBy(p => p.PositionId).Select(p => p.PositionId).ToList()[cb_position.SelectedIndex]
                };

                User user = new User
                {
                    Login = tb_login.Text,
                    Password = tb_password.Text
                };

                var validationResults1 = employee.Validate(new ValidationContext(employee));
                if (validationResults1.Any())
                {
                    string errorMessage = string.Join("\n", validationResults1.Select(r => r.ErrorMessage));
                    MessageBox.Show("Ошибка при сохранении данных сотрудника: " + errorMessage);
                    return;
                }

                var validationResults2 = user.Validate(new ValidationContext(user));
                if (validationResults2.Any())
                {
                    string errorMessage = string.Join("\n", validationResults2.Select(r => r.ErrorMessage));
                    MessageBox.Show("Ошибка при сохранении данных пользователя: " + errorMessage);
                    return;
                }

                TravelAgentsPr21101LyapynovaContext context = TravelAgentsPr21101LyapynovaContext.GetContext();
                context.Users.Add(user);
                context.SaveChanges();

                int userId = user.UserId;
                employee.UserId = userId;

                context.Employees.Add(employee);
                context.SaveChanges();

                MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

       
    }
}