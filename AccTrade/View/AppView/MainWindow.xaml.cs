
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;

namespace AccTrade.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new AddScreen();

        }

        private void MainScreen_btn_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new MainScreen();
        }
    }
}
