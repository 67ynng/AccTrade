
using AccTrade.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace AccTrade.View.AdminView
{
    public partial class UpdateProductWindow : Window
    {
        public void FindProduct()
        {
            using (var context = new AppContext())
            {
                int productId = int.Parse(Id_tb.Text);

                var product = context.Forms.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    Close();
                    var window = new UpdProduct(productId);
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Продукт не найден.");
                }
            }
        }
        public UpdateProductWindow()
        {
            
            InitializeComponent();
        }
        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            FindProduct();
            ////int id;
            ////if (!int.TryParse(Id_tb.Text, out id))
            ////{
            ////    MessageBox.Show("Invalid ID");
            ////    return;
            ////}

            ////// Поиск объекта в БД
            ////using (var context = new AppContext())
            ////{
            ////    var product = context.Forms.FirstOrDefault(p => p.Id == id);
            ////    if (product == null)
            ////    {
            ////        MessageBox.Show("Product not found");
            ////        return;
            ////    }

            ////    // Открытие окна UpdateProduct, передача найденного объекта через конструктор
            ////    var updateWindow = new UpdProduct(product);
            ////    updateWindow.ShowDialog();
            ////}
        }
    }
}
