using AccTrade.Model.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AccTrade.View.AdminView
{
    public partial class AddProductWindow : Window
    {

        
        public AddProductWindow()
        {
            InitializeComponent();
        }
        byte[] imageByte;
        MediaPlayer mediaPlayer = new MediaPlayer();
        string filename;
        private void Price_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 4 || !match.Success)
            {
                e.Handled = true;
            }
        }
        private void open_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Png photo (*.png)|*.png|Jpeg photo (*.jpeg)|*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {

                imageByte = File.ReadAllBytes(openFileDialog.FileName);
                filename = openFileDialog.FileName;
                mediaPlayer.Open(new Uri(filename));
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(imageByte);
                image.EndInit();
                img.Source = image;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {

            string describe = Describe_tb.Text;
            int price = Int32.Parse(Price_tb.Text);
            string game = gamecategory_cb.Text;
          
            string username = User_cb.Text;
            string title = Title_tb.Text;
            Describe_tb.MaxLength= 400;
            Price_tb.MaxLength= 4;
            if (username == "admin")
            {
                MessageBox.Show("This user can't add a product");
            }
            else if(title != null && price != 0 && describe !=null && game != null & username != null) 
            {
                
                using (AppContext db = new AppContext())
                {
                    Add addimg = new Add();
                    addimg.AddImage(imageByte, title, username, game, describe, price);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Enter all data");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        //    if (Describe_tb.Text == "" && Title_tb.Text== "" && gamecategory_cb.Text == "" && User_cb.Text=="" && Price_tb.Text=="")
        //    {
        //        Add_btn.Visibility = Visibility.Visible;

        //    }
        //    else
        //    {
        //        MessageBox.Show(ProductID.ToString());
        //        using (var context = new AppContext())
        //        {
        //            var product = context.Forms.First(p => p.Id == ProductID);
        //            var logins = context.Logins.ToList();
        //            var games = context.Categories.ToList();
        //            if (product != null)
        //            {
        //                gamecategory_cb.ItemsSource = games;
        //                gamecategory_cb.DisplayMemberPath = "GameCategory";
        //                User_cb.ItemsSource = logins;
        //                gamecategory_cb.DisplayMemberPath = "username";
        //                User_cb.Text = product.username;
        //                Title_tb.Text = product.title;
        //                User_cb.Text = product.username;
        //                Price_tb.Text = product.Price.ToString();
        //                Describe_tb.Text = product.Describe;
        //                imageByte = product.ImageData;
        //                BitmapImage image = new BitmapImage();
        //                image.BeginInit();
        //                image.StreamSource = new MemoryStream(imageByte);
        //                image.EndInit();
        //                img.Source = image;
        //            }
        //        }
        //        Add_btn.Visibility = Visibility.Hidden;
        //        Save.Visibility = Visibility.Visible;
        //    }
            using (var db = new AppContext())
            {
                var logins = db.Logins.ToList();
                User_cb.ItemsSource = logins;
                User_cb.DisplayMemberPath = "Username";
                var games = db.Categories.ToList();
                gamecategory_cb.ItemsSource = games;
                gamecategory_cb.DisplayMemberPath = "CategoryName";

            }
        }

        private void Price_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
