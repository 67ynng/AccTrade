using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccTrade.View.AdminView
{
    public partial class UpdUser : Window
    {
        public UpdUser()
        {
            InitializeComponent();
        }
        private string myVariable;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Username_tb.Text == "" || email_tb.Text == "" || phonenum_tb.Text=="")
            {
                MessageBox.Show("Enter all data");
            }
            else
            {
                int id = Int32.Parse(Id_tb.Text);
                myVariable = phonenum_tb.Text;
                string trimmedText = myVariable.Replace(" ", "");
                using (var context = new AppContext())
                {
                    bool phonenumcheck = context.Logins.Any(value => value.PhoneNumber == Convert.ToInt64(trimmedText));

                    var logins = context.Logins.Where(p => p.Id == id).ToList();
                    if (logins.Any())
                    {
                        if (phonenumcheck)
                            MessageBox.Show("This number is already in DataBase");
                        else if (trimmedText.Length < 9  || trimmedText.Length > 10)
                        {
                            MessageBox.Show("Your phone number must be no more than 9-10 characters");
                        }
                        else
                        {
                            foreach (var add in logins)
                            {
                                add.Username = Username_tb.Text;
                                add.Email = email_tb.Text;

                                add.PhoneNumber = Convert.ToInt32(phonenum_tb.Text);
                                MessageBox.Show("update successful");
                                context.SaveChanges();
                                Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("There wasn't this record.");

                    }
                }
            }
        }
        public void FindProduct()
        {
            using (var context = new AppContext())
            {
                int productId = Convert.ToInt32(Id_tb.Text);
                if (productId == 1)
                {
                    MessageBox.Show("You cannot edit this user");

                }
                else
                {
                    var product = context.Logins.FirstOrDefault(p => p.Id == productId);

                    if (product != null)
                    {
                        Username_tb.Visibility = Visibility.Visible;
                        Username_tb.Text = product.Username;
                        email_tb.Text= product.Email;
                        phonenum_tb.Text = Convert.ToString(product.PhoneNumber);
                        email_tb.Visibility = Visibility.Visible;
                        phonenum_tb.Visibility = Visibility.Visible;
                        phonenum_lbl.Visibility = Visibility.Visible;
                        username_lbl.Visibility = Visibility.Visible;
                        email_tb.Visibility = Visibility.Visible;
                        email_lbl.Visibility = Visibility.Visible;
                        Search_btn.Visibility = Visibility.Hidden;
                        updatebtn.Visibility = Visibility.Visible;
                        ID.Visibility= Visibility.Hidden;
                        Id_tb.Visibility= Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("There wasn't this record.");
                    }
                }
            }
        }
        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            bool containsDigits = false;
            foreach (char c in Id_tb.Text)
            {
                if (Char.IsDigit(c))
                {
                    containsDigits = true;
                    break;
                }
            }

            if (containsDigits)
            {
                FindProduct();
            }
            else
            {
                MessageBox.Show("Enter id");
            }

            

        }

        private void phonenum_tb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 13 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void phonenum_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                       phonenum_tb.Paste();
                    }
                }
                e.Handled = true;
            }
        }

        private void Id_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 13 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void Id_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                        Id_tb.Text = clipboardText;
                    }
                }
                e.Handled = true;
            }
        }
    }
}
