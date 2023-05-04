using AccTrade.View.AdminView;
using System.Windows;

namespace AccTrade.View.AppView
{

    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }
        private void User_btn_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new UserPage());
        }

        private void Product_btn_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Product_pages());
        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Games());
        }

        private void Role_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new RolePage());
        }
    }
}
