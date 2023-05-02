using AccTrade.Model.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccTrade.View.AdminView
{
    public partial class UpdateGameWindow : Window
    {
        public UpdateGameWindow()
        {
            InitializeComponent();
        }
        private void OldGamename_tb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 50 || !match.Success)
            {
                e.Handled = true;
            }
        }
        private void OldGamename_tb_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }

        }
        private void Search_tn_Click(object sender, RoutedEventArgs e)
        {
            int idd = Int32.Parse(OldGamename_tb.Text);
            using (var context = new AppContext())
            {
                var products = context.Categories.Where(p => p.Id == idd).ToList();
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
            int idd = Int32.Parse(OldGamename_tb.Text);
            using (var context = new AppContext())
            {
                var products = context.Categories.Where(p => p.Id == idd).ToList();
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.CategoryName = NewGamename_tb.Text;

                    }
                    MessageBox.Show("update successful");
                    context.SaveChanges();
                    Close();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }

        
    }
}
