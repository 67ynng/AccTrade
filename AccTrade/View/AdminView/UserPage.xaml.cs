using AccTrade.Model.Models;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;

namespace AccTrade.View.AdminView
{
    public partial class UserPage : Page
    {
       
        public UserPage()
        {
            InitializeComponent();
            
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteUserWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();

        }
        private void RefreshDataGrid()
        {
            using (var context = new AppContext())
            {
                var data = context.Logins.ToList();
                DataGridUsers.ItemsSource = data;
            }
        }
        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateUserWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }
        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpdUser();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }
    }
}
