using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для ProfileKlient.xaml
    /// </summary>
    public partial class ProfileKlient : Page
    {
        public Klient klient;
        public ProfileKlient(int klientId)
        {
            InitializeComponent();
            klient = TravelAgentsPr21101LyapynovaContext.GetContext().Klients.FirstOrDefault(k=>k.KlientId == klientId);
            LoadKlient(klient);
        }
        public void LoadKlient(Klient klient)
        {
            tb_surname.Text = klient.Surname;
            tb_name.Text = klient.Name;
            tb_patronymic.Text = klient.Patronymic;
            tb_number_phone.Text = klient.PhoneNumber;
            tb_email.Text = klient.Email;
            tb_number_passporta.Text = klient.PassportNumber;
            tb_seria_passporta.Text = klient.PassportSerias;

            var user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.UserId == klient.UserId);
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
        private void btn_history_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HistoryKlient(klient));
        }

        private void btn_save_klient_Click(object sender, RoutedEventArgs e)
        {
            klient.Surname = tb_surname.Text;
            klient.Name = tb_name.Text;
            klient.Patronymic = tb_patronymic.Text;
            klient.PhoneNumber = tb_number_phone.Text;
            klient.Email = tb_email.Text;
            klient.PassportNumber = tb_number_passporta.Text;
            klient.PassportSerias = tb_seria_passporta.Text;

            var user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.UserId == klient.UserId);
            if (user != null)
            {
                user.Login = tb_login.Text;
                user.Password = tb_password.Text;
            }

            var klientValidationResults = klient.Validate(new ValidationContext(klient));
            if (klientValidationResults.Any())
            {
                string errorMessage = string.Join("\n", klientValidationResults.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных клиента: " + errorMessage);
                return;
            }

            var userValidationResults = user.Validate(new ValidationContext(user));
            if (userValidationResults.Any())
            {
                string errorMessage = string.Join("\n", userValidationResults.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных пользователя: " + errorMessage);
                return;
            }

            TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

            MessageBox.Show("Данные о клиенте успешно сохранены.", "Успех", MessageBoxButton.OK);
        }

        private void btn_delete_klient_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                TravelAgentsPr21101LyapynovaContext.GetContext().Klients.Remove(klient);
                TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

                MessageBox.Show("Клиент успешно удален.", "Успех", MessageBoxButton.OK);

            }
        }
    }
}
