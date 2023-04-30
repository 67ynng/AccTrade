using DevExpress.Mvvm.Native;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
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

        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //using (AppContext db = new AppContext())
            //{
            //    var employees = from emp in db.Logins
            //                    select emp;
            //    DataGridUsers.ItemsSource= employees.ToList();
            //}
            _dbContext.Logins.Load();
            DataGridUsers.ItemsSource = _dbContext.Logins.ToObservableCollection();
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }
    }
}
