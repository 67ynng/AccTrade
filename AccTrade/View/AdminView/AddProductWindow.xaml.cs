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
            // Запрещаем ввод любых символов, кроме цифр и запятой
            Regex regex = new Regex("[0-9,]+");
            bool isValid = regex.IsMatch(e.Text);

            // Проверяем, что количество символов не превышает 6 (4 цифры + запятая + 2 цифры после запятой)
            if (((TextBox)sender).Text.Replace(",", "").Length >= 4 && ((TextBox)sender).Text.Contains(",") == false)
            {
                // Если после ввода еще одной цифры количество символов больше 4, ставим запятую
                if ((((TextBox)sender).CaretIndex <= 4) && (((TextBox)sender).Text.Length >= 4))
                {
                    ((TextBox)sender).Text += ",";
                    ((TextBox)sender).CaretIndex = ((TextBox)sender).Text.Length;
                }
            }
            else if (((TextBox)sender).Text.Contains(","))
            {
                // Если вводим цифры после запятой, проверяем количество символов после запятой
                int indexOfDecimalPoint = ((TextBox)sender).Text.IndexOf(",");
                if (indexOfDecimalPoint >= 0 && ((TextBox)sender).Text.Substring(indexOfDecimalPoint + 1).Length >= 2)
                {
                    isValid = false;
                }
            }

            e.Handled = !isValid;
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
            
            string game = gamecategory_cb.Text;
            string username = User_cb.Text;
            string title = Title_tb.Text;
            Describe_tb.MaxLength= 400;
            if (username == "admin")
                MessageBox.Show("This user can't add a product");
            else if (Price_tb.Text == "")
                MessageBox.Show("Enter a price");
            else if (title == "")
                MessageBox.Show("Enter title");
            else if (describe == "")
                MessageBox.Show("Enter describe");
            else if (game == "")
                MessageBox.Show("Pick game");
            else if (username == "")
                MessageBox.Show("Enter username");
            else
            {
                double price = double.Parse(Price_tb.Text);
                double roundedValue = Math.Round(price, 2);
                using (AppContext db = new AppContext())
                {
                    Add addimg = new Add();
                    addimg.AddProduct(imageByte, title, username, game, describe, roundedValue);
                    this.Close();
                }
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

        private void Price_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (Price_tb.Text.Length == 7 && !Price_tb.Text.Contains(","))
            {
                Price_tb.Text = Price_tb.Text.Insert(4, ",");
                Price_tb.CaretIndex = Price_tb.Text.Length;
            }
        }
    }
}
