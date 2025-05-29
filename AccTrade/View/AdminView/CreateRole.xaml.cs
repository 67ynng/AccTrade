using AccTrade.Model.Models;
using System.Linq;
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
            if (Role == "")
                MessageBox.Show("Enter all data");
            else
            {
                using (var db = new AppContext())
                {
                    bool isUserExists = db.Roles.Any(u => u.Role == Role);
                    if (isUserExists)
                        MessageBox.Show("Game is already in DataBase");
                    else
                    {
                        add.AddRole(Role);
                        this.Close();
                    }
                }
            }
        }
    }
}
