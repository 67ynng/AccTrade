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
     public void DeleteProduct(int id)
        {
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Forms.Where(p => p.Id == id).ToList();
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
        public void DeleteUser(int id)
        {
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Logins.Where(p => p.Id == id).ToList();
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
        public void DeleteGame(int Id)
        {
            using (var dbContext = new AppContext())
            {
                var objectsToDelete = dbContext.Categories.Where(p => p.Id == Id).ToList();
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
