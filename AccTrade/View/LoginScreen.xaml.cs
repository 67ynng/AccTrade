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
            if (logtb.Text != "" && passtb.Password != "")
            {
                using (AppContext db = new AppContext())
                {
                    string username = logtb.Text;
                    string password = md5.hashPassword(passtb.Password);
                    var user = db.Logins.Where((u) => u.Username == username && u.Password == password).FirstOrDefault();

                    if (user != null)
                    {
                        MainWindow main = new MainWindow();
                        Close();
                        main.Show();
                    }
                    else MessageBox.Show("Неверные данные!");
                }
            }
            else
            {
                MessageBox.Show("Введите все данные!");
            }
        }
    }
}
