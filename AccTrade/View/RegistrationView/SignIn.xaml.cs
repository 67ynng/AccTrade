using System.Linq;
using AccTrade.Model;
using System.Windows;
using System.Threading;
using Microsoft.Win32;

namespace AccTrade.View.RegistrationView
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignUp_btn_Click(object sender, RoutedEventArgs e)
        {
            SignUp Registartion = new SignUp();
            Close();
            Registartion.Show();
        }

        private void SignIn_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Login_tb.Text != "" && Password_tb.Password != "")
            {
                using (AppContext db = new AppContext())
                {
                    string username = Login_tb.Text;
                    string password = md5.hashPassword(Password_tb.Password);
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
