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
        int count = 2;
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
            if (Login_tb.Text == "admin" && Password_tb.Password == "admin")
            {
                using (AppContext db = new AppContext())
                {
                    var admin = db.Logins.Where((u) => u.Username == "admin" && u.isAdmin == true).FirstOrDefault();
                    if (admin != null)
                    {

                        count--;
                        if (count == 0)
                        {
                            MessageBox.Show("You don't have access to the admin panel");
                        }
                        else if (count >0)
                        {
                            Admin2FA adm2 = new Admin2FA();
                            adm2.Show();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Wrong data");
                    }
                }
            }
            else if (Login_tb.Text != "" && Password_tb.Password != "" || Login_tb.Text != "admin" && Password_tb.Password != "admin")
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
            
            else
            {
                MessageBox.Show("Enter all data!");
            }
           
        }
    }
}
