using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
    /// Логика взаимодействия для AddKlient.xaml
    /// </summary>
    public partial class AddKlient : Page
    {
        public AddKlient()
        {
            InitializeComponent();
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Klient klient = new Klient
                {
                    Surname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tb_surname.Text.Trim()),
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tb_name.Text.Trim()),
                    Patronymic = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tb_patronymic.Text.Trim()),
                    PhoneNumber = tb_number_phone.Text.Trim(),
                    Email = tb_email.Text.Trim(),
                    PassportNumber = tb_number_passporta.Text.Trim(),
                    PassportSerias = tb_seria_passporta.Text.Trim()
                };

                var context = TravelAgentsPr21101LyapynovaContext.GetContext();
                var user = context.Users.FirstOrDefault(u => u.UserId == klient.UserId);
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

                context.SaveChanges();

                MessageBox.Show("Данные о клиенте успешно сохранены.", "Успех", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void btn_save_image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.bmp)|*.png; *.jpg; *.jpeg; *.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                image.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        
    }
}
