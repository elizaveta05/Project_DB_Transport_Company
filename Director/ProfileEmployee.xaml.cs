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
    public partial class ProfileEmployee : Page
    {
        public Employee employee;

        public ProfileEmployee(int EmployeeId)
        {
            InitializeComponent();
            LoadEmployee(EmployeeId);
        }

        private void LoadEmployee(int employeeId)
        {
            employee = TravelAgentsPr21101LyapynovaContext.GetContext().Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                MessageBox.Show("Сотрудник не найден", "Ошибка", MessageBoxButton.OK);
                return;
            }

            tb_surname.Text = employee.Surname;
            tb_name.Text = employee.Name;
            tb_patronymic.Text = employee.Patronymic;
            tb_number_phone.Text = employee.PhoneNumber;

            cb_position.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Positions.Select(p => p.NamePositions).ToList();
            cb_position.SelectedIndex = employee.PositionId - 1;

            User user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.UserId == employee.UserId);
            if (user != null)
            {
                tb_login.Text = user.Login;
                tb_password.Text = user.Password;
            }
            else
            {
                MessageBox.Show("Данные о пользователе не найдены", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var context = TravelAgentsPr21101LyapynovaContext.GetContext();

                // Удаление записи о сотруднике
                context.Employees.Remove(employee);

                // Поиск и удаление записи о пользователе
                User user = context.Users.FirstOrDefault(u => u.UserId == employee.UserId);
                if (user != null)
                {
                    context.Users.Remove(user);
                }

                context.SaveChanges();

                // Замена на другого сотрудника с той же должностью в таблице ServiceAgreement
                Employee newEmployee = context.Employees.FirstOrDefault(e => e.PositionId == employee.PositionId && e.EmployeeId != employee.EmployeeId);

                if (newEmployee != null)
                {
                    var agreementsToUpdate = context.ServiceAgreements.Where(sa => sa.EmployeeId == employee.EmployeeId);
                    foreach (var agreement in agreementsToUpdate)
                    {
                        agreement.EmployeeId = newEmployee.EmployeeId;
                    }

                    context.SaveChanges();

                    MessageBox.Show("Сотрудник успешно удален и заменен на другого сотрудника с такой же должностью.", "Успех", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Сотрудник успешно удален, но не найден другой сотрудник с такой же должностью.", "Успех", MessageBoxButton.OK);
                }

                NavigationService.GoBack();
            }
        }
        private void btn_save_employee_Click(object sender, RoutedEventArgs e)
        {
            employee.Surname = tb_surname.Text;
            employee.Name = tb_name.Text;
            employee.Patronymic = tb_patronymic.Text;
            employee.PhoneNumber = tb_number_phone.Text;
            employee.PositionId = cb_position.SelectedIndex + 1;

            User user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.UserId == employee.UserId);
            if (user != null)
            {
                user.Login = tb_login.Text;
                user.Password = tb_password.Text;
            }

            var validationResults = employee.Validate(new ValidationContext(employee));
            if (validationResults.Any())
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }

            var validationResults1 = user.Validate(new ValidationContext(user));
            if (validationResults1.Any())
            {
                string errorMessage = string.Join("\n", validationResults1.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }

            TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

            MessageBox.Show("Данные о сотруднике успешно сохранены.", "Успех", MessageBoxButton.OK);
        }

        private void cb_position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}