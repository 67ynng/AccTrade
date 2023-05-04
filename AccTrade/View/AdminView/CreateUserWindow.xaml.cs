using AccTrade.Model;
using AccTrade.Model.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccTrade.View.AdminView
{
    public partial class CreateUserWindow : Window
    {
        private string myVariable { get; set; }
        public CreateUserWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Username_tb.Text;
            string password = md5.hashPassword(Password_tb.Password.ToLower());
            string email = email_tb.Text.ToLower();
            myVariable = phonenumber_tb.Text;
            string trimmedText = myVariable.Replace(" ", "");
            string role = Role_cb.Text;
            if (username == "" || Password_tb.Password == "" || email == "" || phonenumber_tb.Text == "")
                MessageBox.Show("Enter all data");
            else if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                MessageBox.Show("Wrong mail format!");
            else
            {
                using (var db = new AppContext())
                {

                    bool isUserExists = db.Logins.Any(u => u.Username == username);
                    bool isEmailExists = db.Logins.Any(u => u.Email == email);
                    bool IsNumberExists = db.Logins.Any(u => u.PhoneNumber == Convert.ToInt32(trimmedText));
                    if (isUserExists)
                        MessageBox.Show("Username is already in DataBase");
                    else if (isEmailExists)
                        MessageBox.Show("Email is already in DataBase");

                    else if (IsNumberExists)
                        MessageBox.Show("Phone number is already in Database");
                    else
                    {
                        if (trimmedText.Length == 10 || trimmedText.Length == 9)
                        {
                            Add add = new Add();
                            add.AddUsers(username, Int32.Parse(trimmedText), password, email, role);
                            MessageBox.Show("User was added in DataBase");
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Number format");
                        }
                    }
                }
            }
        }
        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if (!match.Success || e.Text == " ")
            {
                e.Handled = true;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new AppContext())
            {
                var logins = db.Roles.ToList();
                Role_cb.ItemsSource = logins;
                Role_cb.DisplayMemberPath = "Role";
            }
        }
        private void phonenumber_tb_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    char[] forbiddenChars = new char[] { '|', '@', '.', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
                    string clipboardText = Clipboard.GetText();
                    if (forbiddenChars.Any(c => clipboardText.Contains(c)))
                    {
                        MessageBox.Show("Punctuation and special characters are not allowed");
                        e.Handled = true;
                    }
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                        phonenumber_tb.Text = clipboardText;
                    }
                }
                e.Handled = true;
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private void Username_tb_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void email_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    char[] forbiddenChars = new char[] { '|', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
                    string clipboardText = Clipboard.GetText();
                    if (forbiddenChars.Any(c => clipboardText.Contains(c)))
                    {
                        MessageBox.Show("Punctuation and special characters are not allowed");
                        e.Handled = true;
                    }
                }
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void email_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Username_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
