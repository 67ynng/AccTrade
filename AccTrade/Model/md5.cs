using System;
using System.Security.Cryptography;
using System.Text;

namespace AccTrade.Model
{
   class md5
    {
        public static string hashPassword(string password)
        {
            MD5 md5= MD5.Create();
            byte[]b = Encoding.ASCII.GetBytes(password);
            byte[]hash = md5.ComputeHash(b);
            StringBuilder stringBuilder= new StringBuilder();
            foreach(var a in hash)
                stringBuilder.Append(a  .ToString("X2"));
            return Convert.ToString(stringBuilder);
        }
    }
}
