
using System.Linq;
using System.Windows;

namespace AccTrade.Model.Models
{
    class Add
    {
        public void AddImage(byte[]? imageData, string? title, string? username, string? gameCategory, string? describe,double? price)
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
        public void AddProduct(byte[]? imageData, string? title, string? username, string? gameCategory, string? describe, double? price)
        {
            using (var db = new AppContext())
            {
                var image = new Form
                {

                    username = username,
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
        public void AddUsers(string? name,int? phonenum, string? password, string? email,string? Role)
        {
            using (var db = new AppContext())
            {
                var users = new Login
                {
                    Username = name,
                    PhoneNumber = phonenum,
                    Password = password,
                    Email = email,
                    Role = Role

                };
                db.Logins.AddRange(users);
                db.SaveChanges();
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
        public void AddRole(string? GameName)
        {
            using (var db = new AppContext())
            {
                bool isUserExists = db.Roles.Any(u => u.Role == GameName);
                if (isUserExists)
                {
                    MessageBox.Show("Role is already in DataBase");
                }
                else
                {
                    var game = new Roles
                    {
                        Role = GameName,

                    };
                    db.Roles.AddRange(game);
                    db.SaveChanges();
                }
            }
        }
        public void AddNumber(int Number)
        {
            using (var db = new AppContext())
            {
                bool isUserExists = db.Logins.Any(u => u.PhoneNumber == Number);
                if (isUserExists)
                {
                    MessageBox.Show("Number is already in DataBase");
                }
                else
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
}
