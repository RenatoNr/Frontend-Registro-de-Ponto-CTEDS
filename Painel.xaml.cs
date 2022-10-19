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
using System.Windows.Shapes;

namespace Frontend_Registro_de_Ponto_CTEDS
{
    /// <summary>
    /// Lógica interna para Painel.xaml
    /// </summary>
    public partial class Painel : Window
    {
        public Painel()
        {
            InitializeComponent();
        }

        private async void Cadastro_funcionario(object sender, RoutedEventArgs e)
        {
            var newWindow = new Window1();
            newWindow.Show();
            
        }

        private void Alertas(object sender, RoutedEventArgs e)
        {
            var alertPage = new Alerta();
            alertPage.Show();
        }
        private void Informaçôes_funcionarios(object sender, RoutedEventArgs e)
        {
            var infoPage = new Info();
            infoPage.Show();
        }
        private void Relatorios(object sender, RoutedEventArgs e)
        {
            var newRelatorio = new Relatorio();
            newRelatorio.Show();
        }
    }
}
