using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace AccTrade.Model.Models
{
    class AddImageDB
    {
        public void AddImage(byte[]? imageData  ,string? gameCategory,string? describe,int? price)
        {
            using (var db = new AppContext())
            {
                var image = new Form
                {
                    username = "11111",
                    ImageData = imageData,
                    GameCategory = gameCategory,
                    Describe = describe,
                    Price = price
                };
                db.Forms.AddRange(image);
                db.SaveChanges();
                //bool isExists2 = db.Forms.Any(value => value.Describe == describe);
                //if(isExists2 )
                //    MessageBox.Show("This text is already in database");
                //else
                //{
                   

                //}
            }
        }
    }
}
