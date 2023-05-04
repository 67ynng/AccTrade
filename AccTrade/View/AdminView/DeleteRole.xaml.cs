using AccTrade.Model.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccTrade.View.AdminView
{
    public partial class DeleteRole : Window
    {
        public DeleteRole()
        {
            InitializeComponent();
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            bool containsDigits = false;
            foreach (char c in GameName_tb.Text)
            {
                if (Char.IsDigit(c))
                {
                    containsDigits = true;
                    break;
                }
            }

            if (containsDigits)
            {
                int id = Int32.Parse(GameName_tb.Text);
                if (id == 1)
                {

                    MessageBox.Show("You cannot delete this role");
                }
                else
                {
                    using (var dbContext = new AppContext())
                    {


                        var user = dbContext.Roles.FirstOrDefault(u => u.Id == id);
                        if(user != null) 
                        {
                            var role = dbContext.Roles.FirstOrDefault(r => r.Role == user.Role);
                            if (role != null && role.MembersInThisRole > 1)
                            {
                                MessageBox.Show("Members in this role more than 1");
                            }
                            else
                            {
                                Delete del = new Delete();
                                del.DeleteRole(id);
                                this.Close();
                            }
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
        private void GameName_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                        GameName_tb.Text = clipboardText;
                    }
                }
                e.Handled = true;
            }
        }
        private void GameName_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 8 || !match.Success)
            {
                e.Handled = true;
            }
        }
    }
}
