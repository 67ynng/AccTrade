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
        private void OldGamename_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 8 || !match.Success)
            {
                e.Handled = true;
            }
        }
        private void OldGamename_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                        OldGamename_tb.Text = clipboardText;
                    }
                }
                e.Handled = true;
            }
        }
        private void Search_tn_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(OldGamename_tb.Text, out int idd))
            {
                MessageBox.Show("Enter a valid ID.");
                return;
            }

            using (var context = new AppContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == idd);

                if (category != null)
                {
                    OldGamename_tb.Visibility = Visibility.Hidden;
                    OldGame_lbl.Visibility = Visibility.Hidden;
                    Search_tn.Visibility = Visibility.Hidden;
                    NewGamename_tb.Visibility = Visibility.Visible;
                    NewGame_lbl.Visibility = Visibility.Visible;
                    Update_tn.Visibility = Visibility.Visible;
                    NewGamename_tb.Text = category.CategoryName;
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
        private void Update_tn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppContext())
            {
                if (!int.TryParse(OldGamename_tb.Text, out int idd))
                {
                    MessageBox.Show("Enter a valid ID.");
                    return;
                }

                var product = db.Categories.FirstOrDefault(p => p.Id == idd);
                if (product == null)
                {
                    MessageBox.Show("There wasn't this record.");
                    return;
                }

                string newName = NewGamename_tb.Text.Trim();
                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Enter a name.");
                    return;
                }

                var existingGame = db.Categories.FirstOrDefault(g => g.CategoryName == newName);
                if (existingGame != null && existingGame.Id != idd)
                {
                    MessageBox.Show("A game with this name already exists.");
                    return;
                }

                product.CategoryName = newName;
                db.SaveChanges();
                Close();
            }
        }


    }
}
