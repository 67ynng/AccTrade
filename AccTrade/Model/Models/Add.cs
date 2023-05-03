using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccTrade.Model.Models
{
    class Add
    {
        public void AddImage(byte[]? imageData, string? title, string? username, string? gameCategory, string? describe, int? price)
        {
            using (var db = new AppContext())
            {
                var currentUser = db.Logins.Find(Session.UserId);
                if (currentUser != null) 
                {
                    var image = new Form
                    {

                        username = username,
                        PhoneNumber = currentUser.PhoneNumber,
                        LoginId = currentUser.Id,
                        title = title,
                        ImageData = imageData,
                        GameCategory = gameCategory,
                        Describe = describe,
                        Price = price
                    };
                    db.Forms.AddRange(image);
                    db.SaveChanges();
                }  
            }
        }
        public void AddUsers(string? name,int? phonenum, string? password, string? email, bool? isAdmin)
        {
            using (var db = new AppContext())
            {
                bool isUserExists = db.Logins.Any(u => u.Username == name || u.Email == email|| u.PhoneNumber == phonenum);
                if (isUserExists)
                {
                    MessageBox.Show("User is already in DataBase");
                }
                else
                {
                    var users = new Login
                    {
                        Username = name,
                        PhoneNumber = phonenum,
                        Password = password,
                        Email = email,
                        isAdmin = isAdmin

                    };
                    MessageBox.Show("User was added in DataBase");
                    db.Logins.AddRange(users);
                    db.SaveChanges();
                }

            }
        }
        public void AddGame(string? GameName)
        {
            using (var db = new AppContext())
            {
                bool isUserExists = db.Categories.Any(u => u.CategoryName == GameName);
                if (isUserExists)
                {
                    MessageBox.Show("Game is already in DataBase");
                }
                else
                {
                    var game = new Category
                    {
                        CategoryName = GameName,

                    };
                    MessageBox.Show("Game was added to DataBase");
                    db.Categories.AddRange(game);
                    db.SaveChanges();
                }

            }
        }
        public void AddNumber(int Number)
        {
            using (var db = new AppContext())
            {
                var phoneNum = new Login
                {
                    PhoneNumber = Number
                };
                db.Logins.AddRange(phoneNum);
                db.SaveChanges();
            }
        }
    }
}
