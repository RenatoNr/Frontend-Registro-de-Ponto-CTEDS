using Frontend_Registro_de_Ponto_CTEDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace Frontend_Registro_de_Ponto_CTEDS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient api = new HttpClient();
        private string uri = "https://localhost:7222";
        public BitmapImage  Photo { get; set; } 
        public MainWindow()
        {
            InitializeComponent();
            api.BaseAddress = new Uri(uri);
            api.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            this.Loaded += MainWindow_Loaded1;
            //var image = new ImageBrush();
            //image.ImageSource = new BitmapImage(new Uri("https://spng.pngfind.com/pngs/s/49-491581_clock-icon-clock-blue-png-transparent-png-download.png"));

           // Photo = "https://spng.pngfind.com/pngs/s/49-491581_clock-icon-clock-blue-png-transparent-png-download.png";
        }

        private async void MainWindow_Loaded1(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await api.GetAsync("/api/User");
                //response.EnsureSuccessStatusCode();
                var clocks = await response.Content.ReadAsAsync<IEnumerable<User>>();
                // UsersData.ItemsSource = clocks.ToList();
                // MessageBox.Show( clocks);
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
                // Foto.Source = new BitmapImage(new Uri(user.Result.Photo));
                EmployeePhoto.ImageSource = Photo;

            }
            else
            {
                EmployeePhoto.ImageSource = null;
                Nome.Content = "Cpf não encontrado.";
            }
        }

        private async static Task<HttpResponseMessage> _FindByCPF(string cpf)
        {


            HttpResponseMessage response = await api.GetAsync($"/api/Employee/GetByCpf?cpf={cpf}");
            var content = response.Content.ReadAsAsync<User>();
            //var json = JsonConvert.DeserializeObject<User>(content);
            return response;
        }
    }
}
