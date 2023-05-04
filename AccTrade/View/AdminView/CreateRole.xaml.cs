using AccTrade.Model.Models;
using System.Windows;

namespace AccTrade.View.AdminView
{
    public partial class CreateRole : Window
    {
        public CreateRole()
        {
            InitializeComponent();
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            string Role = GameName_tb.Text;
            Add add = new Add();
            add.AddRole(Role);
            this.Close();
        }
    }
}
