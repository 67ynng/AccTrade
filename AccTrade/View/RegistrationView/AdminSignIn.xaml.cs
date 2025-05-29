using AccTrade.View.AppView;
using System.Linq;
using System.Windows;
using AccTrade.Model;
using DevExpress.Internal;
using System.Windows.Input;
using AccTrade.Model.Models;

namespace AccTrade.View.RegistrationView
{
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
            if (Login_tb.Text == "")
                MessageBox.Show("Enter your admin username");
            else if (Password_tb.Password == "")
                MessageBox.Show("Enter password");
            else if (Login_tb.Text != "" && Password_tb.Password != "")
            {
                using (AppContext db = new AppContext())
                {
                    var user = db.Logins.Where((u) => u.Username == username && u.Password == password && u.Role == "admin").FirstOrDefault();
                    if (user != null)
                    {
                        Session.UserId = user.Id;
                        AdminMainWindow main = new AdminMainWindow();
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
        private void User_btn_Click(object sender, RoutedEventArgs e)
        {
            SignIn sign = new SignIn();
            Close();
            sign.Show();
        }


        private void Login_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char[] forbiddenChars = new char[] { '|', '@', '.', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
            if (forbiddenChars.Contains(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        private void Login_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    char[] forbiddenChars = new char[] { '|', '@', '.', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
                    string clipboardText = Clipboard.GetText();
                    if (forbiddenChars.Any(c => clipboardText.Contains(c)))
                    {
                        e.Handled = true;
                        MessageBox.Show("Punctuation and special characters are not allowed");
                    }
                }
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
