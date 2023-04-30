using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AccTrade.Model.Models
{
    public class MD5Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Преобразование объекта value в строку
            string password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            // Создание объекта MD5
            MD5 md5 = MD5.Create();

            // Преобразование пароля в байтовый массив
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);

            // Хеширование байтового массива
            byte[] hash = md5.ComputeHash(inputBytes);

            // Преобразование хеша обратно в строку
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Вы можете оставить этот метод пустым, если не нужно поддерживать обратное преобразование
            throw new NotImplementedException();
        }
    }
}
