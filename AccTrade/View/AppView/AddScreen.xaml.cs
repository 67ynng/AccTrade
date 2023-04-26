using Microsoft.Win32;
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

namespace AccTrade.View
{
    /// <summary>
    /// Логика взаимодействия для AddScreen.xaml
    /// </summary>
    public partial class AddScreen : Page
    {
        public AddScreen()
        {
            InitializeComponent();
        }
        MediaPlayer mediaPlayer = new MediaPlayer();
        string filename;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Png photo (*.png)|*.png|Jpeg photo (*.jpeg)|*.jpeg";
            bool? dialogOk = openFileDialog.ShowDialog();
            if(dialogOk == true)
            {
                filename = openFileDialog.FileName;
                TextBoxFile_btn.Text = filename;
                mediaPlayer.Open(new Uri(filename));
                img.Source = new BitmapImage(new Uri(filename));
            }

        }

        private void GameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
