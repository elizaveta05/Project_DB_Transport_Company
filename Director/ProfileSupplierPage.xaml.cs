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
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova.Director
{
    /// <summary>
    /// Логика взаимодействия для ProfileSupplierPage.xaml
    /// </summary>
    public partial class ProfileSupplierPage : Page
    {
        private Supplier currentSupplier;
        private int supplierId =0;
        private List<Tour> supplierTours = new List<Tour>();
        public ProfileSupplierPage(int supplierId)
        {
            InitializeComponent();
            this.supplierId=supplierId;
            LoadDate();
        }
        public void LoadDate()
        {
            using (var context = new TravelAgentsPr21101LyapynovaContext())
            {
                currentSupplier = context.Suppliers.FirstOrDefault(s => s.SupplierId == supplierId);

                if (currentSupplier != null)
                {
                    // Вывод данных о поставщике
                    tb_name.Text = currentSupplier.Name;
                    tb_phone.Text = currentSupplier.PhoneNumber;
                    tb_address.Text = currentSupplier.Address;

                    // Поиск данных об учетной записи
                    User user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.UserId == currentSupplier.UserId);
                    if (user != null)
                    {
                        tb_login.Text = user.Login;
                        tb_password.Text = user.Password;
                    }
                    else
                    {
                        MessageBox.Show("Данные о пользователе не найдены", "Ошибка", MessageBoxButton.OK);
                    }

                    // Поиск туров этого поставщика
                    supplierTours = context.Tours.Where(t => t.SupplierId == supplierId).Include(t => t.Country).Include(t => t.City).ToList();
                    tourListView.ItemsSource = supplierTours;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Director.ListSuppliersPage());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (currentSupplier != null)
            {
                using (var context = new TravelAgentsPr21101LyapynovaContext())
                {
                    // Находим все туры, которые принадлежат текущему поставщику и удаляем их
                    List<Tour> toursToRemove = context.Tours.Where(t => t.SupplierId == currentSupplier.SupplierId).ToList();
                    foreach (var tour in toursToRemove)
                    {
                        context.Tours.Remove(tour);
                    }
                    // Находим пользователя, связанного с текущим поставщиком, и удаляем его
                    User userToDelete = context.Users.FirstOrDefault(u => u.UserId == currentSupplier.UserId);
                    if (userToDelete != null)
                    {
                        context.Users.Remove(userToDelete);
                    }

                    // Удаляем поставщика
                    context.Suppliers.Remove(currentSupplier);

                    // Сохраняем изменения в базе данных
                    context.SaveChanges();
                }

                MessageBox.Show("Поставщик и все его туры успешно удалены.");

                // Перенаправляем пользователя на страницу списка поставщиков
                NavigationService.Navigate(new ListSuppliersPage());
            }
            else
            {
                MessageBox.Show("Не удается найти выбранного поставщика для удаления.");
            }
        }

        private void employeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
