﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AccTrade.Model.Models;

namespace AccTrade.View.AdminView.ProductPage
{
    public partial class DeleteProductWindow : Window
    {
        public DeleteProductWindow()
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
            if(containsDigits)
            {
                int id = Int32.Parse(UserId_tb.Text);
                using (var dbContext = new AppContext())
                {
                    var objectsToDelete = dbContext.Forms.Where(p => p.Id == id).ToList();
                    if (objectsToDelete.Any())
                    {
                        Delete del = new Delete();
                        del.DeleteProduct(id);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There wasn't this record.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter id");
            }
        }

        private void UserId_tb_PreviewTextInput(object sender,TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 8 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void UserId_tb_PreviewKeyDown(object sender,KeyEventArgs e)
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
