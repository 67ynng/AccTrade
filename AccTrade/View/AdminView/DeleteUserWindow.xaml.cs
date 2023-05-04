using AccTrade.Model.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccTrade.View.AdminView
{
    public partial class DeleteUserWindow : Window
    {
        public DeleteUserWindow()
        {
            InitializeComponent();
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            bool containsDigits = false;
            foreach (char c in UserId_tb.Text)
            {
                if (Char.IsDigit(c))
                {
                    containsDigits = true;
                    break;
                }
            }
            if (containsDigits)
            {
                int id = Int32.Parse(UserId_tb.Text);
                if (id == 1)
                {
                    MessageBox.Show("You cannot delete this user");
                }
                else
                {
                    using (var dbContext = new AppContext())
                    {
                        var userToDelete = dbContext.Logins.FirstOrDefault(p => p.Id == id);
                        if (userToDelete != null)
                        {
                            var roleName = userToDelete.Role;
                            var roleToUpdate = dbContext.Roles.FirstOrDefault(r => r.Role == roleName);
                            if (roleToUpdate != null && roleToUpdate.MembersInThisRole > 1)
                            {
                                roleToUpdate.MembersInThisRole--;
                                dbContext.Logins.Remove(userToDelete);
                                dbContext.SaveChanges();
                                MessageBox.Show("User deleted successfully");
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Role not found for user");
                            }
                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Enter id");
            }
            
        }

        private void UserId_tb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }

        private void UserId_tb_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                        UserId_tb.Text = clipboardText;
                    }
                }
                e.Handled = true;
            }
        }
    }
}
