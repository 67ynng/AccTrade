using Microsoft.EntityFrameworkCore;

namespace AccTrade.Model.Models
{
    class AddImageDB
    {
        public void AddImage(byte[]? imageData,string? gameCategory,string? describe,int? price)
        {
            using (var db = new AppContext())
            {
                var image = new Form
                {
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
