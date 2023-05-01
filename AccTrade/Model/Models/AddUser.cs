using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AccTrade.View.AdminView;
using Microsoft.EntityFrameworkCore;

namespace AccTrade.Model.Models
{
   public class AddUser
    {
        public void AddUsers(string? name, string? password, string? email, bool? isAdmin)
        {
            using (var db = new AppContext())
            {
                bool isUserExists = db.Logins.Any(u => u.Username == name || u.Email == email);
                if (isUserExists)
                {
                    MessageBox.Show("User is already in DataBase");
                }
                else
                {
                    var users = new Login
                    {
                        Username = name,
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
    }
}
