using AccTrade.Model.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

namespace AccTrade.View.AppView
{
    public partial class EditScreen : Page
    {
        string user;
        private Page _previousPage;
        private int _productId;
        public EditScreen(int productId, Page previousPage)
        {
            InitializeComponent();
            _previousPage = previousPage;
            _productId = productId;
        }
                byte[] imageByte;
        MediaPlayer mediaPlayer = new MediaPlayer();
        string filename;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                var product = context.Forms.FirstOrDefault(p => p.Id == _productId);
                var login = context.Logins.ToList();
                var game = context.Categories.ToList();

                if (product != null)
                {
                    GameBox.ItemsSource = game;
                    GameBox.DisplayMemberPath = "CategoryName";
                    GameBox.Text = product.GameCategory;
                    Title_tb.Text = product.title;
                    PriceTb.Text = product.Price.ToString();
                    DescribeTb.Text = product.Describe;
                    imageByte = product.ImageData;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(imageByte);
                    image.EndInit();
                    img.Source = image;
                }
                else
                {
                    MessageBox.Show("Продукт не найден.");
                }
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
                MessageBox.Show("Add a photo of your product");
            }

        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {

            using (var context = new AppContext())
            {
                var products = context.Forms.Where(p => p.Id == _productId).ToList();
                if (products.Any())
                {
                    foreach (var login in context.Logins)
                    {
                        var userName = login.Username;
                        user = userName;
                    }
                    foreach (var product in products)
                    {

                        product.username = user;
                        product.title= Title_tb.Text;
                        product.Describe = DescribeTb.Text;
                        product.GameCategory  = GameBox.Text;
                        product.Price = Int32.Parse(PriceTb.Text);
                        product.ImageData = imageByte;
                    }

                    MessageBox.Show("Update successful");
                    context.SaveChanges();
                    NavigationService navigationService = NavigationService.GetNavigationService(this);
                    navigationService.Navigate(new ProfileScreen());
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
        private void PriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 4 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void PriceTb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }

        }
    }
}
