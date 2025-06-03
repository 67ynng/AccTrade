using System.Windows;  
namespace AccTrade.View;

using AccTrade.Model.Models;
using AccTrade.View.AppView;
using AccTrade.View.RegistrationView;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Add_btn_Click(object sender, RoutedEventArgs e)
    {
        MyFrame.Navigate(new AddScreen());
      
    }

    private void MainScreen_btn_Click(object sender, RoutedEventArgs e)
    {
        MyFrame.Navigate(new MainScreen());
    }

    private void Profile_btn_Click(object sender, RoutedEventArgs e)
    {
        MyFrame.Navigate(new ProfileScreen());
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        int b;
        b = Session.UserId;

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Session.UserId = 0;
       
        SignIn sign = new SignIn();
        this.Close();
        sign.Show();
    }
}
