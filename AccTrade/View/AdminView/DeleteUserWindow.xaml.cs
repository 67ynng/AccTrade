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
                        var objectsToDelete = dbContext.Logins.Where(p => p.Id == id).ToList();
                        if (objectsToDelete.Any())
                        {
                            Delete del = new Delete();
                            del.DeleteUser(id);

                            this.Close();
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
