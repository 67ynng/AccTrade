using Microsoft.Win32;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using AccTrade.Model.Models;
using System.IO;

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
        double inputValue;
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
        private void DescribeTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {



        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                var PhoneNum = db.Logins.FirstOrDefault(u => u.Id == Session.UserId);
                if (PhoneNum.PhoneNumber == null)
                {
                    MessageBox.Show("Enter your phone number in your account"); return;
                }
                else if (DescribeTb.Text == "" && Title_tb.Text == "" && GameBox.Text == "" && PriceTb.Text == "")
                    MessageBox.Show("Enter all data");
                else if (DescribeTb.Text == "")
                    MessageBox.Show("Enter describe");
                else if (Title_tb.Text == "")
                    MessageBox.Show("Enter title");
                else if (GameBox.Text == "")
                    MessageBox.Show("Choose game");
                else if (PriceTb.Text == "")
                    MessageBox.Show("Enter price");
                else
                {
                    foreach (var login in db.Logins)
                    {
                        var userName = login.Username;
                        user = userName;
                    }
                    string describe = DescribeTb.Text;
                    double price = double.Parse(PriceTb.Text);
                    double roundedValue = Math.Round(price, 2);

                    string game = GameBox.Text;
                    string title = Title_tb.Text;
                    DescribeTb.MaxLength= 400;
                    PriceTb.MaxLength= 4;
                    Add addimg = new Add();
                    addimg.AddImage(imageByte, title, user, game, describe, roundedValue);
                    MessageBox.Show("Your game was add!");
                    NavigationService navigationService = NavigationService.GetNavigationService(this);
                    navigationService.Navigate(new Uri("View\\AppView\\MainScreen.xaml", UriKind.Relative));

                }
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
        private void PriceTb_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
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

        private void PriceTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PriceTb.Text.Length == 7 && !PriceTb.Text.Contains(","))
            {
                PriceTb.Text = PriceTb.Text.Insert(4, ",");
                PriceTb.CaretIndex = PriceTb.Text.Length;
            }
        }
    }
}
