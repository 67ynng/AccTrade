using AccTrade.Model.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AccTrade.View.AdminView
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
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
            using (AppContext db = new AppContext())
            {
                //foreach (var product in db.Forms)
                //{
                //    var userName = product.Username;
                //     = userName;
                //}
                string describe = Describe_tb.Text;
                int price = Int32.Parse(Price_tb.Text);
                string game = gamecategory_cb.Text;
                Describe_tb.MaxLength= 400;
                Price_tb.MaxLength= 4;
                AddImageDB addimg = new AddImageDB();
                addimg.AddImage(imageByte, game, describe, price);
                Close();
            }
        }
    }
}
