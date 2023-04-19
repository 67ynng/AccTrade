using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using AccTrade.Model;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace AccTrade.View
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    string username = logtb.Text;
                    string email = emailtb.Text;
                    string password = md5.hashPassword(passtb.Password);


                    if (email == "" || password == "" || username == "")
                        MessageBox.Show("All fields must be filled!");
                    else if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                        MessageBox.Show("Wrong mail format!");
                    else if (email.Length < 13)
                        MessageBox.Show("Email length cannot be less than 13 characters!");
                    else
                    {
                        using (AppContext db = new AppContext())
                        {
                            db.Database.EnsureCreated();
                            bool isExists = db.Logins.Any(value => value.Email == email);
                            bool isExists2 = db.Logins.Any(value => value.Username == username);

                            if (isExists)
                                MessageBox.Show("This mail is already in the database!");
                            else if (isExists2)
                                MessageBox.Show("This username is already in the database!");
                            else
                            {
                                var user = new Login
                                {
                                    Username = username,
                                    Email = email,
                                    Password = password

                                };

                                db.AddRange(user);
                                db.SaveChanges();
                                MessageBox.Show("Registration completed successfully!");
                                LoginScreen log = new LoginScreen();
                                Close();
                                log.Show();
                            }
                        }
                    }
                });
            })
            {
            }.Start();
        }
    }
}
