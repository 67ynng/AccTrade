using System.Linq;
using System.Windows;

namespace AccTrade.Model.Models
{
    public class Update
    {
        public void UpdateGame(int id,string newgamename)
        {
            using (var context = new AppContext())
            {
                var products = context.Categories.Where(p => p.Id == id).ToList();
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.CategoryName = newgamename;

                    }
                    MessageBox.Show("Update successful");
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }

        }
        public void UpdateRole(int? id, string newgamename)
        {
            using (var context = new AppContext())
            {
                var products = context.Roles.Where(p => p.Id == id).ToList();
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.Role = newgamename;

                    }
                    MessageBox.Show("Update successful");
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }
    }   
}
