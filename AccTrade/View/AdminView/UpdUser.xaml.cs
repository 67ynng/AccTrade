using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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

namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для UpdUser.xaml
    /// </summary>
    public partial class UpdUser : Window
    {
        public UpdUser()
        {
            InitializeComponent();
        }

        private void isadmincheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void isadmincheck_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(Id_tb.Text);   
            using (var context = new AppContext())
            {
                var logins = context.Logins.Where(p => p.Id == id).ToList();
                if (logins.Any())
                {
                    foreach (var add in logins)
                    {
                        add.Username = Username_tb.Text;
                        add.Email = email_tb.Text;
                    }
                    MessageBox.Show("update successful");
                    context.SaveChanges();
                    Close();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
        public void FindProduct()
        {
            using (var context = new AppContext())
            {
                int productId = int.Parse(Id_tb.Text);

                var product = context.Logins.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    Username_tb.Visibility = Visibility.Visible;
                    Username_tb.Text = product.Username;
                    email_tb.Text= product.Email;
                    email_tb.Visibility = Visibility.Visible;
                    username_lbl.Visibility = Visibility.Visible;
                    email_tb.Visibility = Visibility.Visible;
                    Search_btn.Visibility = Visibility.Hidden;
                    updatebtn.Visibility = Visibility.Visible;
                    ID.Visibility= Visibility.Hidden;
                    Id_tb.Visibility= Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Продукт не найден.");
                }
            }
        }
        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            FindProduct();
            

        }
    }
}
