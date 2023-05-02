using AccTrade.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для CreateGameWindow.xaml
    /// </summary>
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
