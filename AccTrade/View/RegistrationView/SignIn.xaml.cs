using System.Linq;
using AccTrade.Model;
using System.Windows;
using AccTrade.Model.Models;
using System.Windows.Input;

namespace AccTrade.View.RegistrationView
{
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
            string password = md5.hashPassword(Password_tb.Password.ToLower());
            if (Login_tb.Text != "" && Password_tb.Password != "")
            {
                using (AppContext db = new AppContext())
                {
                    var user = db.Logins.Where((u) => u.Username == username && u.Password == password && u.Role == "user").FirstOrDefault();
                    if (user != null)
                    {
                        Session.UserId = user.Id;
                        
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

        private void Admin_btn_Click(object sender, RoutedEventArgs e)
        {
            
            AdminSignIn admsgnin = new AdminSignIn();
            Close();
            admsgnin.Show();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                db.Database.EnsureCreated();
                if (!db.Logins.Any(u => u.Username == "admin") && !db.Logins.Any(u => u.Username == "user") && !db.Roles.Any(u => u.Role == "admin") && !db.Roles.Any(u => u.Role == "user") && !db.Categories.Any(u => u.CategoryName == "First Game"))
                {
                    var adminLogin = new Login
                    {
                        Username = "admin",
                        Password = "21232f297a57a5a743894a0e4a801fc3",
                        Role = "admin",
                    };
                    var userlogin = new Login
                    {
                        Username = "user",
                        PhoneNumber = 987654321,
                        Password = "ee11cbb19052e40b07aac0ca060c23ee",
                        Email = "user@gmail.com",
                        Role = "user",
                    };
                    var admin = new Roles
                    {
                        Role = "admin",
                        MembersInThisRole = 1

                    };
                    var user = new Roles
                    {
                        Role = "user",
                        MembersInThisRole = 1
                    };
                    var game = new Category
                    {
                        CategoryName = "FirstName"
                    };
                    db.Logins.AddRange(adminLogin, userlogin);
                    db.Categories.Add(game);
                    db.Roles.AddRange(admin, user);
                    db.SaveChanges();
                }
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

        private void Login_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char[] forbiddenChars = new char[] { '|', '@', '.', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
            if (forbiddenChars.Contains(e.Text[0]))
            {
                e.Handled = true;
            }
        }
    }
}
