using System.Windows;  
namespace AccTrade.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;


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
}
