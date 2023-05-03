using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccTrade.Model.Models
{
    public static class Session
    {
        private static int _userId;
        private static int phoneNumber;
        public static int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public static int phone
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

    }
}
