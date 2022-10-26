using Frontend_Registro_de_Ponto_CTEDS.Models;
using Frontend_Registro_de_Ponto_CTEDS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
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
    /// Lógica interna para Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        private string uri = "https://localhost:7222";
        private List<User> Employees { get; set; } = new List<User>();
        User selectedUser = new User();

        private BitmapImage Photo { get; set; }

        private readonly Api api1;
        public Info()
        {
            InitializeComponent();
            api1 = new Api();
            GetUsers();

        }

     

        private async void GetUsers()
        {
            //using (HttpClient api = new HttpClient())
            //{
            //    api.BaseAddress = new Uri(uri);
            //    api.DefaultRequestHeaders.Add("Accept", "application/json");

            //    var getEmployees = await api.GetAsync("/api/Employee");

            //    var response = await getEmployees.Content.ReadAsStringAsync();

            //    var jsonObject = JsonConvert.DeserializeObject<List<User>>(response);

            //    foreach (var item in jsonObject)
            //    {
            //        Employees.Add(item);
            //    }


            //}

            HttpResponseMessage employees = await api1.Get("/api/Employee");
            var response = await employees.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<List<User>>(response);

            foreach (var item in jsonObject)
            {
                Employees.Add(item);
            }

        }

        private void SearchByName_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SearchByName.Text.Length > 2)
            {
                var users = Employees.Where(x => x.Name.ToLower().Contains(SearchByName.Text.ToLower()));
                if (users != null && SearchByCPF.Text != null)
                {
                    EmployeeDataGrid.ItemsSource = users.ToList();
                    EmployeeDataGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    EmployeeDataGrid.Visibility = Visibility.Collapsed;
                }
            }


            if (SearchByName.Text.Length == 0)
            {
                EmployeeDataGrid.ItemsSource = null;
            }



        }


        private void EmployeeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User cellInfo = EmployeeDataGrid.CurrentCell.Item as User;

            var imageToByte = Convert.FromBase64String(cellInfo.Photo);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(imageToByte);
            image.EndInit();

            EmployeePhoto.ImageSource = image;
            Id.Content = cellInfo.Id;
            Nome.Content = cellInfo.Name;
            CPF.Content = cellInfo.Cpf;
            Cargo.Content = cellInfo.EmployeePost;
        }

        private void SearchByCPF_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SearchByCPF.Text.Length > 2)
            {
                var users = Employees.Where(x => x.Cpf.Contains(SearchByCPF.Text));
                if (users != null && SearchByCPF.Text != null)
                {
                    EmployeeDataGrid.ItemsSource = users.ToList();
                    EmployeeDataGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    EmployeeDataGrid.Visibility = Visibility.Collapsed;
                }
            }

        }
    }




}
