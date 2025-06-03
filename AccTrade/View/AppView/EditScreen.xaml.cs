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
using System.Windows.Navigation;

namespace AccTrade.View.AppView
{
    public partial class EditScreen : Page
    {
        string user;
        private Page _previousPage;
        private int _productId;
        private string _username;
        public EditScreen(int productId,string username, Page previousPage)
        {
            InitializeComponent();
            _previousPage = previousPage;
            _productId = productId;
            _username = username;
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
                    if(imageByte != null)
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.StreamSource = new MemoryStream(imageByte);
                        image.EndInit();
                        img.Source = image;

                    }

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
                mediaPlayer.Open(new Uri(filename));
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(imageByte);
                image.EndInit();
                img.Source = image;
            }

        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {

            using (var context = new AppContext())
            {
                var products = context.Forms.Where(p => p.Id == _productId).ToList();
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.username = _username;
                        product.title= Title_tb.Text;
                        product.Describe = DescribeTb.Text;
                        product.GameCategory  = GameBox.Text;
                        double price = double.Parse(PriceTb.Text);
                        double roundedValue = Math.Round(price, 2);
                        product.Price = roundedValue;
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
            Regex regex = new Regex("[0-9,]+");
            bool isValid = regex.IsMatch(e.Text);
            if (((TextBox)sender).Text.Replace(",", "").Length >= 4 && ((TextBox)sender).Text.Contains(",") == false)
            {
                if ((((TextBox)sender).CaretIndex <= 4) && (((TextBox)sender).Text.Length >= 4))
                {
                    ((TextBox)sender).Text += ",";
                    ((TextBox)sender).CaretIndex = ((TextBox)sender).Text.Length;
                }
            }
            else if (((TextBox)sender).Text.Contains(","))
            {
                int indexOfDecimalPoint = ((TextBox)sender).Text.IndexOf(",");
                if (indexOfDecimalPoint >= 0 && ((TextBox)sender).Text.Substring(indexOfDecimalPoint + 1).Length >= 2)
                {
                    isValid = false;
                }
            }

            e.Handled = !isValid;
        }

        private void PriceTb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }
        }

        private void PriceTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PriceTb.Text.Length == 7 && !PriceTb.Text.Contains(","))
            {
                PriceTb.Text = PriceTb.Text.Insert(4, ",");
                PriceTb.CaretIndex = PriceTb.Text.Length;
            }
        }

        private void GameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
