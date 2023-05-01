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
using AccTrade.Model.Models;

namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для DeleteGameWindow.xaml
    /// </summary>
    public partial class DeleteGameWindow : Window
    {
        public DeleteGameWindow()
        {
            InitializeComponent();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
           string username = GameName_tb.Text;
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Categories.Where(x => x.CategoryName == username).ToList();
                if (objectsToDelete.Any())
                {
                    Delete del = new Delete();
                    del.DeleteGame(username);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }

        }
    }
}
