using AccTrade.Model;
using AccTrade.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string myVariable { get; set; }
        public CreateUserWindow()
        {
            InitializeComponent();
        }
        public bool Adbmin =false;
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
            myVariable = phonenumber_tb.Text;
            string trimmedText = myVariable.Replace(" ", "");
            try
            {
                if (Username_tb.Text == "" || Password_tb.Password == "" || email_tb.Text == "")
                {
                    MessageBox.Show("Error");
                }
                else if (Username_tb.Text != "" || Password_tb.Password != "" || email_tb.Text != "" || phonenumber_tb.Text!="")
                {

                    if (trimmedText.Length == 10)
                    {
                        Add add = new Add();
                        add.AddUsers(username, Int32.Parse(trimmedText), password, email, isAdmin);
                    }
                    else
                    {
                        MessageBox.Show("Wrong Number");
                    }
                    
                   
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 13 || !match.Success)
            {
                e.Handled = true;
            }
        }
    }
}
