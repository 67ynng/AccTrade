using System.Data.SqlClient;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для DeleteUserWindow.xaml
    /// </summary>
    public partial class DeleteUserWindow : Window
    {
        public DeleteUserWindow()
        {
            InitializeComponent();
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Logins.Where(x => x.Username == UserId_tb.Text).ToList();
                if (objectsToDelete.Any())
                {
                    MessageBox.Show("Record was deleted from database");
                    dbContext.Logins.RemoveRange(objectsToDelete);
                }
                else
                {
                    MessageBox.Show("There are wasn't this record");
                }
                dbContext.SaveChanges();
            }
        }
    }
}
