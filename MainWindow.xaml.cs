using Frontend_Registro_de_Ponto_CTEDS.Models;
using Frontend_Registro_de_Ponto_CTEDS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Frontend_Registro_de_Ponto_CTEDS
{

    public partial class MainWindow : Window
    {
        private string Logo = "https://spng.pngfind.com/pngs/s/49-491581_clock-icon-clock-blue-png-transparent-png-download.png";
        public BitmapImage Photo { get; set; }
        private User Employee { get; set; }

        private Api _api;
        public MainWindow()
        {
            _api = new Api();
            InitializeComponent();

            this.Loaded += MainWindow_Loaded1;
            Photo = new BitmapImage(new Uri(Logo));
        }

        private async void MainWindow_Loaded1(object sender, RoutedEventArgs e)
        {
            try
            {

                var response = await _api.Get("/api/User");

                var clocks = await response.Content.ReadAsAsync<IEnumerable<User>>();

            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao tentar conectar a API");
            }
        }

        private BitmapImage ConvertBase64Photo(string photo)
        {
            var imageToByte = Convert.FromBase64String(photo);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(imageToByte);
            image.EndInit();
            return image;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var cpf = Cpf.Text;
            HttpResponseMessage response = await _api.Get($"/api/Employee/GetByCpf?cpf={cpf}");
            var user = response.Content.ReadAsAsync<User>();

            if (user.Result.Cpf != null)
            {
                var img = ConvertBase64Photo(user.Result.Photo);


                Photo = img;
                Nome.Content = user.Result.Name;
                ClockGrid.Visibility = Visibility.Visible;
                Hours.Visibility = Visibility.Visible;
                EmployeePhoto.ImageSource = Photo;

                HttpResponseMessage clocks = await _api.Get($"/api/Clock/GetByEmployee?employeeId={user.Result.Id}");


                var today = clocks.Content.ReadAsStringAsync().Result;
                var s = JsonConvert.DeserializeObject(today);

            }
            else
            {
                EmployeePhoto.ImageSource = null;
                Nome.Content = "Cpf não encontrado.";
                ClockGrid.Visibility = Visibility.Hidden;
                Hours.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Painel painel = new Painel();
            painel.Show();
        }




    }
}
