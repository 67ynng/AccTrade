using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            int id=  Int32.Parse(UserId_tb.Text);
            Delete del = new Delete();
            del.DeleteProduct(id);
            this.Close();
        }

        private void UserId_tb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 50 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void UserId_tb_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }
        }
    }
}
