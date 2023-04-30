using AccTrade.Model;
using AccTrade.Model.Models;
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

namespace AccTrade.View.AdminView
{
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow()
        {
            InitializeComponent();
        }
        public bool Adbmin =false;

        public void AddRecord(Login newRecord)
        {
            using (var db = new AppContext())
            {
                db.Logins.Add(newRecord);
                db.SaveChanges();
            }
        }
        private void isadmincheck_Checked(object sender, RoutedEventArgs e)
        {
            Adbmin = true;
        }
        private void isadmincheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Adbmin = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Username_tb.Text;
            string password = md5.hashPassword(Password_tb.Password);
            string email = email_tb.Text;
            bool? isAdmin = Adbmin;
            AddUser add = new AddUser();
            add.AddUsers(username, password, email, isAdmin);
            MessageBox.Show("Record added to database.");
        }

       
    }
}
