﻿using AccTrade.Model.Models;
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
            add.AddGame(GameName);
            this.Close();
        }
    }
}
