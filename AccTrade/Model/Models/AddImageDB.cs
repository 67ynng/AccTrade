using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace AccTrade.Model.Models
{
    class AddImageDB
    {
        public void AddImage(byte[]? imageData,string? title,string? username,string? gameCategory,string? describe,int? price)
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
    }
}
