using AccTrade.View.AppView;
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
using AccTrade.Model;
using System.IO;

namespace AccTrade.View.RegistrationView
{
    /// <summary>
    /// Логика взаимодействия для AdminSignIn.xaml
    /// </summary>
    public partial class AdminSignIn : Window
    {
        public AdminSignIn()
        {
            InitializeComponent();
        }
        private void SignIn_btn_Click(object sender, RoutedEventArgs e)
        {
            string username = Login_tb.Text;
            string password = md5.hashPassword(Password_tb.Password);
            using (AppContext db = new AppContext())
            {
                var admin = db.Logins.Where((u) => u.isAdmin == true ).FirstOrDefault();
                if(admin != null)
                {
                    AdminMainWindow adm = new AdminMainWindow();
                    Close();
                    adm.Show();
                }
                else
                {
                    MessageBox.Show("You are not admin");
                }

            }
        }
        private void User_btn_Click(object sender, RoutedEventArgs e)
        {
            SignIn sign = new SignIn();
            Close();
            sign.Show();
        }

        private void FACode_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
