
using System.Windows;
using Microsoft.SqlServer;
namespace AccTrade.View
{
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void regbtn_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            Close();
            reg.Show();
        }
    }
}
