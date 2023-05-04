using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AccTrade.View.AdminView
{
    public partial class Games : Page
    {
        public Games()
        {
            InitializeComponent();
        }
        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateGameWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteGameWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();


        }
        private void RefreshDataGrid()
        {
            using (var context = new AppContext())
            {
                var data = context.Categories.ToList();
                DataGridGames.ItemsSource = data;
            }
        }
        private void Update_btn_Click(object sender, RoutedEventArgs e) 
        {
            var window = new UpdateGameWindow();
            window.Closed += (s, eventArgs) => RefreshDataGrid();
            window.Show();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                DataGridGames.ItemsSource =context.Categories.ToList();
            }
        }
    }
}
