using System.Linq;
using AccTrade.Model;
using System.Windows;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;
using AccTrade.View.AppView;
using System.Windows.Controls.Ribbon;

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

            string username = Login_tb.Text;
            string password = md5.hashPassword(Password_tb.Password);
            if (Login_tb.Text != "" && Password_tb.Password != "" && Login_tb.Text != "admin")
            {
                using (AppContext db = new AppContext())
                {
                    var user = db.Logins.Where((u) => u.Username == username && u.Password == password).FirstOrDefault();

                    if (user != null)
                    {
                        MainWindow main = new MainWindow();
                        Close();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Wrong data");
                    }

                }
            }
            else if (Login_tb.Text == "admin")
            {
                MessageBox.Show("You can't login here as administrator"); 
            }

            else
            {
                MessageBox.Show("Enter all data!");
            }

        }

        private void Admin_btn_Click(object sender, RoutedEventArgs e)
        {
            
            AdminSignIn admsgnin = new AdminSignIn();
            Close();
            admsgnin.Show();
        }
    }
}
