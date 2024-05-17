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

namespace Travel_agency_Lyapynova.Director
{
    /// <summary>
    /// Логика взаимодействия для AddSupplierPage.xaml
    /// </summary>
    public partial class AddSupplierPage : Page
    {
        public AddSupplierPage()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Supplier newSupplier = new Supplier
            {
                Name = tb_name.Text,
                PhoneNumber = tb_phone.Text,
                Address = tb_address.Text
            };

            var supplierValidationResults = newSupplier.Validate(new ValidationContext(newSupplier));
            if (supplierValidationResults.Any())
            {
                string errorMessage = string.Join("\n", supplierValidationResults.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных поставщика: " + errorMessage);
                return;
            }

            var userValidationResults = newSupplier.User.Validate(new ValidationContext(newSupplier.User));
            if (userValidationResults.Any())
            {
                string errorMessage = string.Join("\n", userValidationResults.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных пользователя: " + errorMessage);
                return;
            }

            using (var context = new TravelAgentsPr21101LyapynovaContext())
            {
                context.Suppliers.Add(newSupplier);
                context.SaveChanges();
            }

            MessageBox.Show("Поставщик успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService?.GoBack();
        }
    }
}
