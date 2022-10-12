using Frontend_Registro_de_Ponto_CTEDS.Models;
using Frontend_Registro_de_Ponto_CTEDS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public Relatorio()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:7222");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.Loaded += Relatorio_Loaded;
        }

        async void Relatorio_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync("/api/Clock");
                var clocks = await response.Content.ReadAsAsync<IEnumerable<Clock>>();
                reportClock.ItemsSource = clocks;

            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao tentar conectar a API");
            }
        }
    }
}
