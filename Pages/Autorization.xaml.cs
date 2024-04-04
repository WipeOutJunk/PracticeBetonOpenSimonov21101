using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace PracticeBetonNetV.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = UsernameTextBox.Text;
                var password = HashPassword(PasswordBox.Password);
                Console.WriteLine(password);

                using (var context = new PracticeBetonContext())
                {
                    var employee = context.Employees
                        .Where(e => e.Login == login && e.Password == password)
                        .Include(e => e.Role)
                        .FirstOrDefault();
                    
                    if (employee != null)
                    {
                        
                        switch (employee.RoleId)
                        {
                            case 1:
                                NavigationService.Navigate(new clientsPage());
                                break;
                            case 2:
                                NavigationService.Navigate(new qualitiControl(employee.RoleId)); //readonly
                                break;
                            case 7:
                                NavigationService.Navigate(new suppliersPage(employee.RoleId));
                                break;
                            case 8:
                                NavigationService.Navigate(new orderPage(employee.RoleId)); //readonly
                                break;
                            case 6:
                                NavigationService.Navigate(new orderPage(employee.RoleId)); //readonly
                                break;
                            case 5:
                               // Console.WriteLine("good " + employee.FirstName);
                              NavigationService.Navigate(new qualitiControl(employee.RoleId));
                                break;
                            
                            default:
                                MessageBox.Show("Недопустимая роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль введены неверно.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Вычисление хеша
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Преобразование байтового массива в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
