using Frontend_Registro_de_Ponto_CTEDS.Models;
using Frontend_Registro_de_Ponto_CTEDS.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
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
        private int EmployeeId { get; set; }


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

                ApiError.Visibility = Visibility.Visible;
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


                EmployeeId = user.Result.Id;
                Photo = img;
                Nome.Content = user.Result.Name;
                ClockGrid.Visibility = Visibility.Visible;
                Hours.Visibility = Visibility.Visible;

                EmployeePhoto.ImageSource = Photo;

                HttpResponseMessage clocks = await _api.Get($"/api/Clock/GetTodayClock?employeeId={user.Result.Id}");

                var today = await clocks.Content.ReadAsStringAsync(); //.ReadAsStringAsync().Result.Replace("\\","").Replace("[]","").Trim(new char[1] {'"'});

                var s = JsonConvert.DeserializeObject<Clock>(today);
                checkInClock.Content = s.ClockIn;
                lunchOutClock.Content = s.LunchOut;
                lunchInClock.Content = s.LunchIn;
                checkOutClock.Content = s.ClockOut;

            }
            else
            {
                EmployeePhoto.ImageSource = null;
                Nome.Content = "Cpf não encontrado.";
                ClockGrid.Visibility = Visibility.Hidden;


                Hours.Visibility = Visibility.Hidden;
                EmployeeId = 0;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Painel painel = new Painel();
            painel.Show();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {

            var getClocks = await _GetEmployeeClocks(EmployeeId);


            if (getClocks.ClockIn == null)
            {
                Clock clock = new Clock();
                var now = DateTime.Now;
                clock.EmployeeId = EmployeeId;
                clock.ClockIn = now;
                clock.TotalHours = "";

                var response = await _api.CreateClock(clock, "/api/Clock/");
                if (response != null)
                {
                    MessageBox.Show("Registro de entrada efetuado com sucesso");
                    return;
                }
            }

            if (getClocks.ClockIn != null && getClocks.LunchIn != null && getClocks.LunchOut ==null)
            {
                HttpContent content = new StringContent("", Encoding.UTF8, "application/json");

                var response = await _api.UpdateTime($"/api/Clock/UpdateTime?Id={getClocks.Id}&update=1", content);
                if (response != null)
                {
                    MessageBox.Show("Registro de entrada efetuado com sucesso");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Nenhum registro de saída encontrado");
                return;

            }



        }

        private async Task<Clock> _GetEmployeeClocks(int id)
        {
            try
            {
                HttpResponseMessage getClocks = await _api.Get($"/api/Clock/GetTodayClock?employeeId={EmployeeId}");
                var content = await getClocks.Content.ReadAsStringAsync();
                var clocks = JsonConvert.DeserializeObject<Clock>(content);
                return clocks;
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var getClocks = await _GetEmployeeClocks(EmployeeId);


            if (getClocks.LunchIn == null)
            {
                HttpContent content = new StringContent("", Encoding.UTF8, "application/json");

                var response = await _api.UpdateTime($"/api/Clock/UpdateTime?Id={getClocks.Id}&update=0", content);
                if (response != null)
                {
                    MessageBox.Show("Registro de Saída para almoço efetuado com sucesso");
                    return;
                }
            }

            if (getClocks.LunchOut != null && getClocks.ClockOut == null)
            {
                HttpContent content = new StringContent("", Encoding.UTF8, "application/json");

                var response = await _api.UpdateTime($"/api/Clock/UpdateTime?Id={getClocks.Id}&update=2", content);
                if (response != null)
                {
                    MessageBox.Show("Registro de Saída de expediente efetuado com sucesso");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Nenhum registro de saída encontrado ou carga horária já preenchida");
                return;

            }


        }
    }
}
