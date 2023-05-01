using AccTrade.Model.Models;
using System.Linq;
using System.Windows;
namespace AccTrade.View.AdminView
{
    public partial class UpdateGameWindow : Window
    {
        public UpdateGameWindow()
        {
            InitializeComponent();
        }
        private void Search_tn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                var products = context.Categories.Where(p => p.CategoryName.Contains(OldGamename_tb.Text)).ToList();
                if (products.Any())
                {
                    OldGamename_tb.Visibility= Visibility.Hidden;
                    OldGame_lbl.Visibility= Visibility.Hidden;
                    Search_tn.Visibility= Visibility.Hidden;
                    NewGamename_tb.Visibility= Visibility.Visible;
                    NewGame_lbl.Visibility= Visibility.Visible;
                    Update_tn.Visibility= Visibility.Visible;
                    NewGamename_tb.Text= OldGamename_tb.Text;
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
        private void Update_tn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                var products = context.Categories.Where(p => p.CategoryName.Contains(OldGamename_tb.Text)).ToList();
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.CategoryName = NewGamename_tb.Text;

                    }
                    MessageBox.Show("update successful");
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
    }
}
