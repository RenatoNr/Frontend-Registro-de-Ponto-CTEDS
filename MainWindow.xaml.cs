using Frontend_Registro_de_Ponto_CTEDS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Frontend_Registro_de_Ponto_CTEDS
{

    public partial class MainWindow : Window
    {
        static HttpClient api = new HttpClient();
        private string uri = "https://localhost:7222";
        private string Logo = "https://spng.pngfind.com/pngs/s/49-491581_clock-icon-clock-blue-png-transparent-png-download.png";
        public BitmapImage Photo { get; set; }
        private User Employee { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            api.BaseAddress = new Uri(uri);
            api.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            this.Loaded += MainWindow_Loaded1;
            Photo = new BitmapImage(new Uri(Logo));
        }

        private async void MainWindow_Loaded1(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await api.GetAsync("/api/User");

                var clocks = await response.Content.ReadAsAsync<IEnumerable<User>>();

            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao tentar conectar a API");
            }
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var cpf = Cpf.Text;
            HttpResponseMessage response = await api.GetAsync($"/api/Employee/GetByCpf?cpf={cpf}");
            var user = response.Content.ReadAsAsync<User>();

            if (user.Result.Cpf != null)
            {
                Photo = new BitmapImage(new Uri(user.Result.Photo));
                Nome.Content = user.Result.Name;
                ClockGrid.Visibility = Visibility.Visible;
                Hours.Visibility = Visibility.Visible;
                EmployeePhoto.ImageSource = Photo;

                HttpResponseMessage clocks = await api.GetAsync($"/api/Clock/GetByEmployee?employeeId={user.Result.Id}");

               
                    var today =  clocks.Content.ReadAsStringAsync().Result;
                    var s = JsonConvert.DeserializeObject(today);
                

               // var jsonString = JsonSerializer.DeserializeAsync<Clock>(today);
                
                //clockIn.Content = today.Result.ClockIn ;

                //EmployeeData.ItemsSource = user;

            }
            else
            {
                EmployeePhoto.ImageSource = null;
                Nome.Content = "Cpf não encontrado.";
                ClockGrid.Visibility = Visibility.Hidden;
                Hours.Visibility = Visibility.Hidden;
            }
        }

        private async static Task<HttpResponseMessage> _FindByCPF(string cpf)
        {

            HttpResponseMessage response = await api.GetAsync($"/api/Employee/GetByCpf?cpf={cpf}");
            var content = response.Content.ReadAsAsync<User>();

            return response;
        }
    }
}
