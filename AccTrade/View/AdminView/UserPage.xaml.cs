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
using System.Xml.Linq;

namespace AccTrade.View.AdminView
{
    public partial class UserPage : Page
    {
        private readonly AppContext _dbContext = new AppContext();
        public UserPage()
        {
            InitializeComponent();
            
        }


        public void AddRecord(Login newRecord)
        {
            using (var db = new AppContext())
            {
                db.Logins.Add(newRecord);
                db.SaveChanges();
            }
        }

        public void DeleteRecord(int recordId)
        {
            using (var db = new AppContext())
            {
                var recordToDelete = db.Logins.Find(recordId);
                if (recordToDelete != null)
                {
                    db.Logins.Remove(recordToDelete);

                    // уменьшаем ID для всех записей после удаленной
                    var recordsToDecrement = db.Logins.Where(r => r.Id > recordId);
                    foreach (var record in recordsToDecrement)
                    {
                        record.Id--;
                    }

                    db.SaveChanges();
                }
            }
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e) => Page_Loaded(sender, e);

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
        //    var selectedObject = DataGridUsers.SelectedItem as Login;

        //    if (selectedObject != null)
        //    {
        //        using (var context = new AppContext())
        //        {
        //            var lastLoginId = context.Logins.Max(login => login.Id);
        //            MessageBox.Show(lastLoginId.ToString()); // Выведет значение 7
        //        }
        //        _dbContext.Logins.Remove(selectedObject);
        //        _dbContext.SaveChanges();

        //    }
        //using(var db = new AppContext())
        //{
        //    var selectedItems = DataGridUsers.SelectedItems;
        //    foreach (var selectedItem in selectedItems)
        //    {
        //        var item = selectedItem as Login;
        //        var id = item.Id; // здесь Id должен быть свойством, хранящим идентификатор вашей сущности

        //        var entityToDelete = db.Logins.FirstOrDefault(x => x.Id == id);
        //        if (entityToDelete != null)
        //        {
        //            db.Logins.Remove(entityToDelete);
        //        }
        //    }
        //}
        //using (var context = new AppContext())
        //{
        //    var selectedItems = DataGridUsers.SelectedItems;

        //    foreach (var selectedItem in selectedItems)
        //    {
        //        var item = selectedItem as Login;
        //        var id = item.Id;

        //        var entityToDelete = context.Logins.FirstOrDefault(x => x.Id == id);
        //        if (entityToDelete != null)
        //        {
        //            context.Logins.Remove(entityToDelete);
        //        }
        //    }

        //    context.SaveChanges();
        //}
        //if (DataGridUsers.SelectedItems.Count > 0)
        //{
        //    foreach (var item in DataGridUsers.SelectedItems)
        //    {
        //        Login entity = item as Login;

        //        using (var context = new AppContext())
        //        {
        //            var existingEntity = context.Logins.Find(entity.Id);
        //            context.Logins.Remove(existingEntity);
        //            context.SaveChanges();
        //        }
        //    }


        //}
        //DataGridUsers.Items.Refresh();
        DeleteUserWindow deleteuser = new DeleteUserWindow();
        deleteuser.Show();

        }

    private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow create = new CreateUserWindow();
            create.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //using (AppContext db = new AppContext())
            //{
            //    var employees = from emp in db.Logins
            //                    select emp;
            //    DataGridUsers.ItemsSource= employees.ToList();
            //}
            //_dbContext.Logins.Load();
            //DataGridUsers.ItemsSource = _dbContext.Logins.ToObservableCollection();
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
