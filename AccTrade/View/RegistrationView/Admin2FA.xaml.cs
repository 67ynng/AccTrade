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
using System.Windows.Shapes;
using System.IO;
using System.Runtime.CompilerServices;
using AccTrade.View.RegistrationView;
using System.Threading;

namespace AccTrade.View.AppView
{
    /// <summary>
    /// Логика взаимодействия для Admin2FA.xaml
    /// </summary>
    public partial class Admin2FA : Window
    {
        public Admin2FA()
        {
            InitializeComponent();
            
        }
        int count = 3;
        private void Prove_btn_Click(object sender, RoutedEventArgs e)
        {
            count = count -1;
            atmpts_lbl.Content= count;
            string[] content = File.ReadAllLines("2FA.txt");
            if (TestAdmin_tb.Text != content[0])
            {
                
                MessageBox.Show("Wrong 2FA Code\nAttempts:" + count);
                if(count == 0) 
                {
                    MessageBox.Show("You are not an administrator");
                    Close();
                }
                
            }
            else if (TestAdmin_tb.Text == content[0])
            {
                SignIn sgn = new SignIn();
                AdminMainWindow adm = new AdminMainWindow();
                Close();
                sgn.Close();
                adm.Show();
            }
        }
    }
}
