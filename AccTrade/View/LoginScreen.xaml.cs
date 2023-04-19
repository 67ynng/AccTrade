using System.Linq;
using AccTrade.Model;   
using System.Windows;
using System.Threading;
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

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
