using AccTrade.Model.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Windows;
namespace AccTrade.View.AdminView
{
    public partial class CreateGameWindow : Window
    {
        public CreateGameWindow()
        {
            InitializeComponent();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            string GameName = GameName_tb.Text;
            Add add = new Add();
            
            
            if (GameName == "")
                MessageBox.Show("Enter all data");
            else
            {
                using (var db = new AppContext())
                {

                    bool isUserExists = db.Categories.Any(u => u.CategoryName == GameName);
                    if (isUserExists)
                        MessageBox.Show("Game is already in DataBase");
                    else
                    {
                        add.AddGame(GameName);
                        this.Close();
                    }
                }
            }
        }
    }
}
