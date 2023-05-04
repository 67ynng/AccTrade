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
            bool containsDigits = false;
            foreach (char c in OldGamename_tb.Text)
            {
                if (Char.IsDigit(c))
                {
                    containsDigits = true;
                    break;
                }
            }

            if (containsDigits)
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

                    }
                    else
                    {
                        MessageBox.Show("There wasn't this record.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter id.");
            }
           
        }
        private void Update_tn_Click(object sender, RoutedEventArgs e)
        {
            if(NewGamename_tb.Text.Contains(""))
            {
                MessageBox.Show("Enter a name.");
            }
            else
            {
                using (var context = new AppContext())
                {
                    int idd = Int32.Parse(OldGamename_tb.Text);
                    string NewName = NewGamename_tb.Text;
                    Update upd = new Update();

                    var existingGame = context.Categories.FirstOrDefault(g => g.CategoryName == NewName);

                    if (existingGame != null)
                    {
                        MessageBox.Show("A game with this name already exists.");
                    }
                    else
                    {
                        upd.UpdateGame(idd, NewName);
                        Close();
                    }
                }
            }
           
            
        }


    }
}
