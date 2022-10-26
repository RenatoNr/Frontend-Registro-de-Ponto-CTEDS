using Frontend_Registro_de_Ponto_CTEDS.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Media.Imaging;


namespace Frontend_Registro_de_Ponto_CTEDS
{
    
    public partial class Window1 : Window
    {
        private BitmapImage Photo;
        private Api _api;
        private string uri = "https://localhost:7222";
        public Window1()
        {
            //_api = new Api();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog photo = new OpenFileDialog();

            photo.Title = "Selecione uma foto do funcionário";
            photo.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
               "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
               "Portable Network Graphic (*.png)|*.png";

            if (photo.ShowDialog() == true)
            {
                var img = new BitmapImage(new Uri(photo.FileName));
                imgPhoto.Source = img;
                Photo = img;

            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Photo == null)
            {
                MessageBox.Show("Deve ser adicionada a foto do funcionário.");
                return;
            }
            var image = ImageToByte(Photo);

            var imagem2 = Convert.ToBase64String(image);
     
            var employee = new Models.User()
            {                
                Name = EmployeeName.Text,
                Cpf = Cpf.Text,
                Password = Password.Text,
                Photo = imagem2 ,
                EmployeePost = EmployeePost.Text
            };

            using (HttpClient api = new HttpClient())
            {
                api.BaseAddress=new Uri(uri);
                api.DefaultRequestHeaders.Add("Accept", "application/json");
                

                var response = await api.PostAsJsonAsync("/api/Employee",employee);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Novo funcionário cadastrado com sucesso!");
                    Close();
                   
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro. Verifique se os dados estão corretos.");
                }
            }
                                 
        }

        private static byte[] ImageToByte(BitmapImage imageSource)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageSource));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
