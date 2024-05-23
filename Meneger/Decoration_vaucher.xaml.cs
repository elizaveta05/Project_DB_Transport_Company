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
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova.Meneger
{
    public partial class Decoration_vaucher : Page
    {
        Klient klient;
        public int employeeId;

        public Decoration_vaucher(Klient klient, int employeeId)
        {
            InitializeComponent();
            this.klient = klient;
            klient = TravelAgentsPr21101LyapynovaContext.GetContext().Klients.FirstOrDefault(k => k.KlientId == klient.KlientId);
            LoadKlient(klient);
            this.employeeId = employeeId;

        }

        public void LoadKlient(Klient klient)
        {
            tb_surname_klient.Text = klient.Surname;
            tb_name_klient.Text = klient.Name;
            tb_patromyc.Text = klient.Patronymic;
            tb_phone.Text = klient.PhoneNumber;
            tb_mail.Text = klient.Email;
            tb_passport.Text = klient.PassportNumber + " / " + klient.PassportSerias;
        }
        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";

            if (string.IsNullOrEmpty(tb_name_dogovor.Text))
            {
                errorMessage += "- Не заполнено наименование договора\n";
            }

            if (klient == null)
            {
                errorMessage += "- Клиент не выбран\n";
            }

            if (employeeId == 0)
            {
                errorMessage += "- Сотрудник не выбран\n";
            }

            if (string.IsNullOrEmpty(errorMessage))
            {
                Employee employee = TravelAgentsPr21101LyapynovaContext.GetContext().Employees.FirstOrDefault(emp => emp.EmployeeId == employeeId);

                int maxid = TravelAgentsPr21101LyapynovaContext.GetContext().ServiceAgreements.Max(em => em.ContractId);
                ServiceAgreement s = new ServiceAgreement
                {
                    ContractId = maxid + 1,
                    KlientId = klient.KlientId,
                    EmployeeId = employee.EmployeeId,
                    DateOfConclusion = DateOnly.FromDateTime(DateTime.Today),
                    Conditions = tb_name_dogovor.Text.Trim()
                };

                using (var context = new TravelAgentsPr21101LyapynovaContext())
                {
                    context.ServiceAgreements.Add(s);
                    context.SaveChanges();

                    MessageBox.Show("Договор создан!");

                    // Переход на страницу Decoration_vaucher1 с передачей ID нового договора
                    Decoration_vaucher1 page = new Decoration_vaucher1(s.ContractId);
                    this.NavigationService.Navigate(page);
                }
            }
            else
            {
                MessageBox.Show($"Проверьте данные:\n{errorMessage}", "Ошибка", MessageBoxButton.OK);
            }
        }
      
    }
}