using Frontend_Registro_de_Ponto_CTEDS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Frontend_Registro_de_Ponto_CTEDS
{
    public partial class Alerta : Window
    {
        DateTime _date = DateTime.Parse("19 / 10 / 2022");

        private string uri = "https://localhost:7222";
        private List<User> Employees { get; set; } = new List<User>();
        private List<WorkDay> WorkDays { get; set; } = new List<WorkDay>();
        public Alerta()
        {

            InitializeComponent();
            GetWorksDay();
            GetFaltas();
            this.Loaded += Alertas_Loaded;
        }
        private async void GetFaltas()
        {
            
            bool excedeulimitefaltas = false;
            using (HttpClient api = new HttpClient())
            {
                api.BaseAddress = new Uri(uri);
                api.DefaultRequestHeaders.Add("Accept", "application/json");

                var getEmployees = await api.GetAsync("/api/Employee");

                var response = await getEmployees.Content.ReadAsStringAsync();

                var jsonObject = JsonConvert.DeserializeObject<List<User>>(response);

                foreach (var item in jsonObject)
                {
                    Employees.Add(item);
                }
                foreach (var employee in Employees)
                {
                    int faltas = 0;
                    var workdays = WorkDays.Where(x => x.EmployeeId == employee.Id);
                    foreach (var workday in workdays)
                    {
                        faltas++;
                    }
                    employee.faltas = faltas;
                }
                foreach (var employee in Employees)
                {
                    if (employee.faltas > 5)
                    {
                        employee.excedeulimite = "sim";
                        excedeulimitefaltas = true;
                        MessageBox.Show("Trabalhador " + employee.Name + ",cpf:" + employee.Cpf + " faltou mais que o limite permitido");
                    }
                }
                if (excedeulimitefaltas)
                {
                    MessageBox.Show("Trabalhador faltou mais que o limite permitido");
                }

            }
        }

        private async void GetWorksDay()
        {
            using (HttpClient api = new HttpClient())
            {
                api.BaseAddress = new Uri(uri);
                api.DefaultRequestHeaders.Add("Accept", "application/json");

                var getWorksDay = await api.GetAsync("/api/WorkDay");

                var response = await getWorksDay.Content.ReadAsStringAsync();

                var jsonObject = JsonConvert.DeserializeObject<List<WorkDay>>(response);

                foreach (var item in jsonObject)
                {
                    WorkDays.Add(item);
                }

            }
        }

        async void Alertas_Loaded(object sender, RoutedEventArgs e)
        {
            reportAlerta.ItemsSource = Employees;

        }

    }
}
