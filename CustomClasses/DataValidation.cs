using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PracticeBetonNetV.CustomClasses
{
   

    public static class DataValidation
    {
        // Метод для проверки номера телефона
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = new Regex(@"^\+\d{11}$");
            return phoneRegex.IsMatch(phoneNumber);
        }

        // Метод для проверки почтового адреса
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@]+@[^@]+\.[^@]+$");
            return emailRegex.IsMatch(email);
        }

        // Метод для проверки физического адреса
        public static bool IsValidAddress(string address)
        {
            // Это пример регулярного выражения; он может потребовать настройки под ваши конкретные требования
            var addressRegex = new Regex(@"^.+,\s*ул\.\s*[^,]+,\s*д\.\s*\d+$");
            return addressRegex.IsMatch(address);
        }
    }
}
