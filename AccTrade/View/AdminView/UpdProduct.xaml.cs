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
    public partial class UpdProduct : Window
    {

        public UpdProduct()
        {
            InitializeComponent();
        }
        byte[] imageByte;
        MediaPlayer mediaPlayer = new MediaPlayer();
        string filename;
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
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {

            int id = Int32.Parse(Id_tb.Text);
            string usercb = User_cb.Text;
            string title = Title_tb.Text;
            string describe = Describe_tb.Text;
            string game = gamecategory_cb.Text;
            double price = double.Parse(Price_tb.Text);
            double roundedValue = Math.Round(price, 2);
            if (User_cb.Text == "admin")
                MessageBox.Show("This user can't add a product");
            else
            {
                using (var context = new AppContext())
                {
                    var products = context.Forms.Where(p => p.Id == id).ToList();
                    if (products.Any())
                    {
                        foreach (var product in products)
                        {
                            Update upd = new Update();
                            product.username = usercb;
                            product.title = title;
                            product.Describe = describe;
                            product.GameCategory = game;
                            product.Price = roundedValue;
                            product.ImageData = imageByte;

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
            
        }

        private void Price_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
        public void FindProduct()
        {
            bool containsDigits = false;
            foreach (char c in Id_tb.Text)
            {
                if (Char.IsDigit(c))
                {
                    containsDigits = true;
                    break;
                }
            }

            if (containsDigits)
            {
                using (var context = new AppContext())
                {
                    int productId = int.Parse(Id_tb.Text);

                    var product = context.Forms.FirstOrDefault(p => p.Id == productId);
                    var login = context.Logins.ToList();
                    var game = context.Categories.ToList();

                    if (product != null)
                    {
                        open_btn.Visibility = Visibility.Visible;
                        Title_tb.Visibility = Visibility.Visible;
                        gamecategory_cb.Visibility = Visibility.Visible;
                        User_cb.Visibility = Visibility.Visible;
                        Describe_tb.Visibility = Visibility.Visible;
                        Price_tb.Visibility = Visibility.Visible;
                        img.Visibility = Visibility.Visible;
                        photo_lbl.Visibility = Visibility.Visible;
                        price_lbl.Visibility = Visibility.Visible;
                        Title_lbl.Visibility = Visibility.Visible;
                        describe_lbl.Visibility=  Visibility.Visible;
                        photo_lbl.Visibility = Visibility.Visible;
                        game_lbl.Visibility = Visibility.Visible;
                        user_lbl.Visibility = Visibility.Visible;   
                        ID.Visibility = Visibility.Hidden;
                        Id_tb.Visibility = Visibility.Hidden;
                        Search_btn.Visibility= Visibility.Hidden;
                        this.Width = 600;
                        this.Height = 400;
                        this.WindowStartupLocation=WindowStartupLocation.CenterScreen;

                        gamecategory_cb.ItemsSource = game;
                        gamecategory_cb.DisplayMemberPath = "CategoryName";

                        User_cb.ItemsSource = login;
                        User_cb.DisplayMemberPath = "Username";
                        User_cb.Text = product.username;
                        gamecategory_cb.Text = product.GameCategory;
                        Title_tb.Text = product.title;
                        User_cb.Text = product.username;
                        Price_tb.Text = product.Price.ToString();
                        Describe_tb.Text = product.Describe;
                        imageByte = product.ImageData;
                        if (imageByte == null)
                        {

                        }
                        else
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
                        MessageBox.Show("There wasn't this record.");
                    }
                }
                }
            else
            {
                MessageBox.Show("Enter id");
            }
        }
        private void Price_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                        Id_tb.Text = clipboardText;
                    }
                }
                e.Handled = true;
            }

        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            FindProduct();
        }

        private void Id_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 8 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void Id_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();
                    bool isNumeric = int.TryParse(clipboardText, out _);
                    if (isNumeric)
                    {
                        Id_tb.Text = clipboardText;
                    }
                }
                e.Handled = true;
            }
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
