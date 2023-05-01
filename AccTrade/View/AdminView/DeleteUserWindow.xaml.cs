using AccTrade.Model.Models;
using System.Linq;
using System.Windows;


namespace AccTrade.View.AdminView
{
    public partial class DeleteUserWindow : Window
    {
        public DeleteUserWindow()
        {
            InitializeComponent();
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            string username = UserId_tb.Text;
            Delete del = new Delete();
            del.DeleteUser(username);
            this.Close();
        }
    }
}
