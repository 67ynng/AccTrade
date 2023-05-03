using AccTrade.Model.Models;
using AccTrade.View.AdminView;
using DevExpress.Mvvm.Native;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                var user = context.Logins.FirstOrDefault(u => u.Id == Session.UserId);
                var forms = context.Forms.Where(f => f.LoginId == Session.UserId).ToList();
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
            if ((sender as TextBox).Text.Length >= 13 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void phonenumber_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                myVariable = phonenumber_tb.Text;
                string trimmedText = myVariable.Replace(" ", "");
                e.Handled = true;
                if(trimmedText.Length == 10) 
                {
                    //Session.phone = Int32.Parse(trimmedText);
                    MessageBox.Show("Your number was added");
                    using (var db = new AppContext())
                    {
                        
                        Login user = db.Logins.Find(Session.UserId);
                        if (user != null)
                        {
                            user.PhoneNumber = Int32.Parse(trimmedText);
                           
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Your number is already in DataBase");
                        }
                        //Form form = db.Forms.FirstOrDefault(f => f.username == username);
                        //if (form != null)
                        //{
                        //    form.PhoneNumber = Int32.Parse(trimmedText);
                        //    db.SaveChanges();
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("Your phone number must be no more than 10 characters");
                }
            }
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Form)MyProducts.SelectedItem;
            if (selectedProduct != null)
            {
                int productId = selectedProduct.Id;
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                navigationService.Navigate(new EditScreen(productId, this));
            }
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Form)MyProducts.SelectedItem;
            if (selectedProduct != null)
            {
                int productId = selectedProduct.Id;
                Delete del = new Delete();
                if (MessageBox.Show("Are you sure you want to remove this item?","Delete",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    del.DeleteProduct(productId);
                    NavigationService navigationService = NavigationService.GetNavigationService(this);
                    navigationService.Navigate(new ProfileScreen());
                }
            }
        }
    }
}
