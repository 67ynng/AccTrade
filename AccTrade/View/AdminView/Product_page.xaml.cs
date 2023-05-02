using AccTrade.View.AdminView.ProductPage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для Product_pages.xaml
    /// </summary>
    public partial class Product_pages : Page
    {
        public Product_pages()
        {
            InitializeComponent();
        }
        private void RefreshDataGrid()
        {
            using (var context = new AppContext())
            {
                var data = context.Forms.ToList();
                DataGridProduct.ItemsSource = data;
            }
        }
        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddProductWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteProductWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new AppContext())
            {
                var employees = from emp in db.Forms
                                select emp;
                DataGridProduct.ItemsSource= employees.ToList();
                var images = db.Forms.ToList();
                DataGridProduct.DataContext = new { Images = images };
                DataGridProduct.ItemsSource = images;
            }

        }
        private void DataGridProduct_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            if (DataGridProduct.IsReadOnly)
            {
                e.NewItem = false;
            }
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpdProduct();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }
    }
}
