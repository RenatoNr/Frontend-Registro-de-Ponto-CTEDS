using Frontend_Registro_de_Ponto_CTEDS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
  
    public partial class PasswordConfirm : Window
    {
        public string Pass { get; set; }
        public PasswordConfirm()
        {
            InitializeComponent();
        }

      
        private  void  Button_Click(object sender, RoutedEventArgs e)
        {
            
            Pass = Password.Password;
            Close();
                        

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(Password.Password.Length > 3)
            {
                RegisterButton.IsEnabled = true;
            }
            else
            {
                RegisterButton.IsEnabled = false;
            }
           
        }
    }
}
