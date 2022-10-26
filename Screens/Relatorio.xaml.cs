using Frontend_Registro_de_Ponto_CTEDS.Models;
using Frontend_Registro_de_Ponto_CTEDS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// <summary>
    /// Lógica interna para Relatorio.xaml
    /// </summary>
    public partial class Relatorio : Window
    {
        HttpClient client = new HttpClient();
        private string uri = "https://localhost:7222";
        private List<User> Employees { get; set; } = new List<User>();
        public Relatorio()
        {
            InitializeComponent();
            GetUsers();
            client.BaseAddress = new Uri("https://localhost:7222");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.Loaded += Relatorio_Loaded;
        }
        private async void GetUsers()
        {
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


            }


        }
        async void Relatorio_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync("/api/Clock");
                var clocks = await response.Content.ReadAsAsync<IEnumerable<Clock>>();
                foreach(var clock in clocks)
                {
                    var users = Employees.Where(x => x.Id == clock.EmployeeId);
                    var user = users.ToList();
                    foreach (var item in user)
                    {
                        if(clock.EmployeeId == item.Id)
                        {
                            clock.Name = item.Name;
                            clock.Cpf = item.Cpf;
                        }
                    }
                }
                reportClock.ItemsSource = clocks;

            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao tentar conectar a API");
            }
        }
    }
}
