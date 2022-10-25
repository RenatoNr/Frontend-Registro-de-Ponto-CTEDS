
using System.Windows;


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
