using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AccTrade.Model.Models;

namespace AccTrade.View.AdminView.ProductPage
{
    /// <summary>
    /// Логика взаимодействия для DeleteProductWindow.xaml
    /// </summary>
    public partial class DeleteProductWindow : Window
    {
        public DeleteProductWindow()
        {
            InitializeComponent();
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            string username=  UserId_tb.Text;
            Delete del = new Delete();
            del.DeleteProduct(username);
            this.Close();
        }
    }
}
