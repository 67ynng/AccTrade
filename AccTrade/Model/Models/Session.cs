﻿
namespace AccTrade.Model.Models
{
    public static class Session
    {
        private static int _userId;
        public static int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private static string _Username;
        public static string UserName
        {

            get { return _Username; }
            set { _Username = value; }
        }

    }
}
