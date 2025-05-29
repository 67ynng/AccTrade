using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace AccTrade.View.AdminView
{
    public partial class RolePage : Page
    {
        public RolePage()
        {
            InitializeComponent();
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpdRole();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }
        private void RefreshDataGrid()
        {
            using (var context = new AppContext())
            {
                var data = context.Roles.ToList();
                DataGridRole.ItemsSource = data;
            }
        }
        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateRole();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();

        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteRole();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                DataGridRole.ItemsSource =context.Roles.ToList();
            }
        }
    }
}
