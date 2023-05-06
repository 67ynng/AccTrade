
using AccTrade.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AccTrade.View
{
    public partial class MainScreen : Page
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            int userId = Session.UserId;
            using (var db = new AppContext())
            {
                ListVVV.ItemsSource =  db.Forms.ToList();
                var user = db.Logins.FirstOrDefault(u => u.Id == userId); ;
            }
        }
    }
}
