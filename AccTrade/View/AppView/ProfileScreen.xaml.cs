using AccTrade.Model.Models;
using DevExpress.Mvvm.Native;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AccTrade.View.AppView
{
    public partial class ProfileScreen : Page
    {
        public ProfileScreen()
        {
            InitializeComponent();
        }
        private string myVariable;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                var user = context.Logins.FirstOrDefault(u => u.Id == Session.UserId || u.Username == Session.UserName);
                var forms = context.Forms.Where(f => f.LoginId == Session.UserId || f.username == Session.UserName).ToList();
                if (user != null)
                {
                    lblusername.Content = user.Username;
                    lblemail.Content = user.Email;
                    phonenumber_tb.Text = user.PhoneNumber.ToString();
                    MyProducts.ItemsSource = forms;
                }
            }
        }

        private void phonenumber_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if (!match.Success || e.Text == " ")
            {
                e.Handled = true;
            }
        }
        public void phoneNum()
        {
            myVariable = phonenumber_tb.Text;
            string trimmedText = myVariable.Replace(" ", "");
            if (trimmedText.Length < 9)
            {
                MessageBox.Show("Your phone number must be no more than 9 characters");
            }
            else
            {
                using (var db = new AppContext())
                {
                    bool phonenumcheck = db.Logins.Any(value => value.PhoneNumber == Convert.ToInt64(trimmedText));
                    if (phonenumcheck)
                        MessageBox.Show("This number is already in DataBase");
                    else
                    {
                        Login user = db.Logins.Find(Session.UserId);
                        if (user != null)
                        {
                            MessageBox.Show("Your number was changed");
                            user.PhoneNumber =Convert.ToInt32(trimmedText);
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Your number is already in DataBase");
                        }
                    }
                    
                }
            }
        }

        private void phonenumber_tb_PreviewKeyDown(object sender, KeyEventArgs e)
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
            if (e.Key == Key.Enter)
            {
                phoneNum();
                e.Handled = true;
            }
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {

            Button editButton = sender as Button;
            if (editButton != null)
            {
                var selectedProduct = editButton.DataContext as Form;
                if (selectedProduct != null)
                {
                    int productId = selectedProduct.Id;
                    string username = selectedProduct.username;
                    NavigationService navigationService = NavigationService.GetNavigationService(this);
                    navigationService.Navigate(new EditScreen(productId,username, this));
                }
            }
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            if (editButton != null)
            {
                var selectedProduct = editButton.DataContext as Form;
                if (selectedProduct != null)
                {
                    int productId = selectedProduct.Id;
                    Delete del = new Delete();
                    if (MessageBox.Show("Are you sure you want to remove this item?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        del.DeleteProduct(productId);
                        NavigationService navigationService = NavigationService.GetNavigationService(this);
                        navigationService.Navigate(new ProfileScreen());
                    }
                }
            }
        }

        private void SaveNumber_btn_Click(object sender, RoutedEventArgs e)
        {
            phoneNum(); 
        }

        private void MyProducts_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
