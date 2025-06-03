
using AccTrade.Model.Models;
using AccTrade.View.AppView;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AccTrade.View
{
    public partial class MainScreen : Page
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            int userId = Session.UserId;
            using (var db = new AppContext())
            {
                ListVVV.ItemsSource = db.Forms.ToList();
                var user = db.Logins.FirstOrDefault(u => u.Id == userId); ;
            }
        }
        private void Contact_btn_Click(object sender, RoutedEventArgs e)
        {
            var selectedForm = ListVVV.SelectedItem as Form;
            if (selectedForm == null)
            {
                MessageBox.Show("Please select a listing from the list first.", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int userId = selectedForm.LoginId;
            using (var db = new AppContext())
            {
                var user = db.Logins.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    string email = user.Email;
                    int? phoneNumber = user.PhoneNumber;

                    MessageBox.Show($"Contact details:\nEmail: {email}\nPhone number: {phoneNumber}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No user found for the selected listing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }
}
