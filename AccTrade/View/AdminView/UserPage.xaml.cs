using AccTrade.Model.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace AccTrade.View.AdminView
{
    public partial class UserPage : Page
    {
        private readonly AppContext _dbContext = new AppContext();
        public UserPage()
        {
            InitializeComponent();
            
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e) => Page_Loaded(sender, e);

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteUserWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();

        }
        private void RefreshDataGrid()
        {
            using (var context = new AppContext())
            {
                var data = context.Logins.ToList();
                DataGridUsers.ItemsSource = data;
            }
        }
        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateUserWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                var customers = context.Logins.ToList();
                DataGridUsers.ItemsSource = customers;
            }
        }
        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }

        private void DataGridUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
