using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AccTrade.Model.Models;

namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для DeleteGameWindow.xaml
    /// </summary>
    public partial class DeleteGameWindow : Window
    {
        public DeleteGameWindow()
        {
            InitializeComponent();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
           int id = Int32.Parse(GameName_tb.Text);
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Categories.Where(p => p.Id == id).ToList();
                if (objectsToDelete.Any())
                {
                    Delete del = new Delete();
                    del.DeleteGame(id);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }

        }

        private void GameName_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }
        }

        private void GameName_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 50 || !match.Success)
            {
                e.Handled = true;
            }
        }
    }
}
