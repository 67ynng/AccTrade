using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccTrade.View.AdminView
{
    public partial class UpdRole : Window
    {
        public UpdRole()
        {
            InitializeComponent();
        }
        private void OldGamename_tb_PreviewTextInput(object sender,TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 8 || !match.Success)
            {
                e.Handled = true;
            }
        }
        private void OldGamename_tb_PreviewKeyDown(object sender,KeyEventArgs e)
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
                if (idd == 1)
                {
                    MessageBox.Show("You cannot edit this role");
                }
                else
                {
                    using (var context = new AppContext())
                    {
                        var products = context.Roles.FirstOrDefault(p => p.Id == idd);

                        if (products != null)
                        {
                            OldGamename_tb.Visibility= Visibility.Hidden;
                            OldGame_lbl.Visibility= Visibility.Hidden;
                            Search_tn.Visibility= Visibility.Hidden;
                            NewGamename_tb.Visibility= Visibility.Visible;
                            NewGame_lbl.Visibility= Visibility.Visible;
                            Update_tn.Visibility= Visibility.Visible;
                            NewGamename_tb.Text = products.Role;
                        }
                        else
                        {
                            MessageBox.Show("There wasn't this record.");
                        }
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Enter id");
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

                var product = db.Roles.FirstOrDefault(p => p.Id == idd);
                if (product == null)
                {
                    MessageBox.Show("There wasn't this record.");
                    return;
                }

                string newName = NewGamename_tb.Text.Trim();
                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Enter a role.");
                    return;
                }

                var existingGame = db.Roles.FirstOrDefault(g => g.Role == newName);
                if (existingGame != null && existingGame.Id != idd)
                {
                    MessageBox.Show("A game with this name already exists.");
                    return;
                }

                product.Role = newName;
                db.SaveChanges();
                Close();
            }
        }
    
    }
}
