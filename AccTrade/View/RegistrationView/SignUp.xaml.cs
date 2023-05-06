using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AccTrade.Model;
using AccTrade.Model.Models;

namespace AccTrade.View.RegistrationView
{
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private string myVariable;
        private void Application_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char[] forbiddenChars = new char[] { '|', '@', '.','#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-',',','+','=','?'};
            if (forbiddenChars.Contains(e.Text[0]))
            {
                e.Handled = true;
            }
        }
        //private void Application_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    // Проверяем, является ли нажатая комбинация клавиш CTRL + V
        //    if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
        //    {
        //        // Получаем вставляемый текст из буфера обмена
        //        string clipboardText = Clipboard.GetText();

        //        // Удаляем запрещенные символы из вставляемого текста
        //        char[] forbiddenChars = new char[] { '|', '@', '.', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
        //        clipboardText = new string(clipboardText.Where(c => !forbiddenChars.Contains(c)).ToArray());

        //        // Вставляем отредактированный текст в TextBox
        //        var textBox = (TextBox)sender;
        //        var text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
        //        textBox.Text = text.Insert(textBox.SelectionStart, clipboardText);

        //        // Отменяем обработку клавиши, чтобы символы не вставились в TextBox
        //        e.Handled = true;
        //    }
        //}
        private void SignIn_btn_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    
                    myVariable = PhoneNumber_tb.Text;
                    string trimmedText = myVariable.Replace(" ", "");
                    string username = Login_tb.Text;
                    string email = Email_tb.Text;
                    string password = md5.hashPassword(Password_tb.Password.ToLower());
                    string passwordConfirm = md5.hashPassword(PassConfirm_tb.Password.ToLower());
                    int phoneNum = 0;
                    
                    string role = "user";
                    if (email == "" || password == "" || username == "" || PassConfirm_tb.Password == "" || PhoneNumber_tb.Text == "")
                        MessageBox.Show("All fields must be filled!");
                    else if (!Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+(\.[\w-]+)*$"))
                        MessageBox.Show("Wrong mail format!");
                    else if (password != passwordConfirm)
                        MessageBox.Show("Password mismatch");
                    else if (trimmedText.Length < 9)
                        MessageBox.Show("Invalid phone number format");
                    else
                    {
                        using (AppContext db = new AppContext())
                        {
                            int pp = Convert.ToInt32(trimmedText);

                            var currentMembers = db.Roles.FirstOrDefault(r => r.Role == "user")?.MembersInThisRole ?? 0;
                            var newMembers = currentMembers + 1;
                            bool isExists = db.Logins.Any(value => value.Email == email);
                            bool isExists2 = db.Logins.Any(value => value.Username == username);
                            bool phonenumcheck = db.Logins.Any(value => value.PhoneNumber == phoneNum);
                            if (isExists)
                                MessageBox.Show("This mail is already in the database!");
                            else if (isExists2)
                                MessageBox.Show("This username is already in the database!");
                            else if (phonenumcheck)
                                MessageBox.Show("This phone number is already in the database!");
                            else
                            {
                                var add = new Login
                                {
                                    Role = role,
                                    Username = username,
                                    Email = email,
                                    Password = passwordConfirm,
                                    PhoneNumber = pp,
                                };
                                var rolle = db.Roles.FirstOrDefault(r => r.Role == "user");
                                if (rolle != null)
                                {
                                    rolle.MembersInThisRole = newMembers;
                                    db.SaveChanges();
                                }
                                db.AddRange(add);
                                db.SaveChanges();
                                MessageBox.Show("Registration completed successfully!");
                                SignIn log = new SignIn();
                                Close();
                                log.Show();
                            }
                        }
                    }
                });
            })
            {
            }.Start();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            SignIn sign = new SignIn();
            Close();
            sign.Show();
        }

        private void PhoneNumber_tb_PreviewKeyDown(object sender, KeyEventArgs e)
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
                    }
                }
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void PhoneNumber_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if (!match.Success || e.Text == " ")
            {
                e.Handled = true;
            }
        }

        private void Email_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char[] forbiddenChars = new char[] { '|', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
            if (forbiddenChars.Contains(e.Text[0]))
            {
                MessageBox.Show("Punctuation and special characters are not allowed");
                e.Handled = true;
            }
        }

        private void Login_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char[] forbiddenChars = new char[] { '|', '@','.', '#', '\'', '`', '/', '\\', '{', '}', '[', ']', ';', '>', '<', ',', ':', ';', '$', '!', '%', '^', '&', '*', '(', ')', '_', '"', '-', ',', '+', '=', '?' };
            if (forbiddenChars.Contains(e.Text[0]))
            {
                MessageBox.Show("Punctuation and special characters are not allowed");
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

        private void Email_tb_PreviewKeyDown(object sender, KeyEventArgs e)
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
    }
}
