using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AccTrade.View.AdminView;

namespace AccTrade.Model.Models
{
    public class Delete 
    {
        public void DeleteProduct(string username)
        {
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Forms.Where(x => x.title == username).ToList();
                if (objectsToDelete.Any())
                {
                    var userToDelete = objectsToDelete.First();
                    dbContext.Forms.RemoveRange(objectsToDelete);
                    dbContext.SaveChanges();
                    MessageBox.Show("Record was deleted from database.");
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
        public void DeleteUser(string username)
        {
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Logins.Where(x => x.Username == username).ToList();
                if (objectsToDelete.Any())
                {
                    var userToDelete = objectsToDelete.First();
                    dbContext.Logins.RemoveRange(objectsToDelete);
                    dbContext.SaveChanges();
                    MessageBox.Show("Record was deleted from database.");
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
        public void DeleteGame(string username)
        {
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Categories.Where(x => x.CategoryName == username).ToList();
                if (objectsToDelete.Any())
                {
                    var userToDelete = objectsToDelete.First();
                    dbContext.Categories.RemoveRange(objectsToDelete);
                    dbContext.SaveChanges();
                    MessageBox.Show("Record was deleted from database.");
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }

    }
   
}
