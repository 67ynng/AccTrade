using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var users = new Login
                {
                    Username= name,
                    Password= password,
                    Email= email,
                    isAdmin=isAdmin
                };
                db.Logins.AddRange(users);
                db.SaveChanges();
            }
        }
    }
}
