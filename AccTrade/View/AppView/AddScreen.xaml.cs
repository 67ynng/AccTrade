using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using AccTrade.Model.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Channels;
using AccTrade.View.AdminView;

namespace AccTrade.View
{
    public partial class AddScreen : Page
    {
        string user;
        public AddScreen()
        {
            InitializeComponent();
        }
        byte[] imageByte;
        MediaPlayer mediaPlayer = new MediaPlayer();
        string filename;
        private void PriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 4 || !match.Success)
            {
                e.Handled = true;
            }

        }
        private void Open_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Png photo (*.png)|*.png|Jpeg photo (*.jpeg)|*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {

                imageByte = File.ReadAllBytes(openFileDialog.FileName);
                filename = openFileDialog.FileName;
                TextBoxFile_btn.Text = filename;
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
        private void DescribeTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {



        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            using(AppContext db = new AppContext())
            {
                foreach (var login in db.Logins)
                {
                    var userName = login.Username;
                    user = userName;
                }
                string describe = DescribeTb.Text;
                int price = Int32.Parse(PriceTb.Text);
                string game = GameBox.Text;
                string title = Title_tb.Text;
                DescribeTb.MaxLength= 400;
                PriceTb.MaxLength= 4;
                Add addimg = new Add();
                addimg.AddImage(imageByte,title,user, game, describe, price);
                NavigationService.GoBack();
            }
            
           

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new AppContext())
            {
                var games = db.Categories.ToList();
                GameBox.ItemsSource = games;
                GameBox.DisplayMemberPath = "CategoryName";

            }
        }
    }
}
