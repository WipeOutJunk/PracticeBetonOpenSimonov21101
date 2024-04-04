using PracticeBetonNetV.CustomClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PracticeBetonNetV.Pages
{
    /// <summary>
    /// Логика взаимодействия для clientsPage.xaml
    /// </summary>
    public partial class clientsPage : Page
    {
        private ObservableCollection<Client> ClientsData = new ObservableCollection<Client>();

        public clientsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var context = new PracticeBetonContext())
                {
                    var data = context.Clients.OrderBy(id => id.ClientId).ToList();
                    ClientsData.Clear();
                    foreach (var item in data)
                    {
                        ClientsData.Add(item);
                    }
                }
                dataGrid.ItemsSource = ClientsData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new PracticeBetonContext())
                {
                    foreach (var data in ClientsData)
                    {
                        if (data.ClientId == 0) // Предполагая, что новые записи имеют qc_id равный 0
                        {
                            if (!DataValidation.IsValidPhoneNumber(data.Phone))
                            {
                                MessageBox.Show($"Неверный формат телефона: {data.Phone}");
                                LoadData();
                                return; // Пропускаем текущую итерацию цикла
                            }

                            if (!DataValidation.IsValidEmail(data.Email))
                            {
                                MessageBox.Show($"Неверный формат электронной почты: {data.Email}");
                                LoadData();
                                return; // Пропускаем текущую итерацию цикла
                            }
                            context.Clients.Add(data);
                        }
                    }
                    context.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = dataGrid.SelectedItems.Cast<Client>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new PracticeBetonContext())
                    {
                        foreach (var data in selectedItems)
                        {

                            var entity = context.Clients.Find(data.ClientId);
                            if (entity != null)
                            {
                                context.Clients.Remove(entity);
                            }
                        }
                        context.SaveChanges();
                    }

                    MessageBox.Show("Записи успешно удалены.");
                    LoadData(); // Обновляем данные в DataGrid после удаления
                }
                else
                {
                    MessageBox.Show("Выберите запись для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new PracticeBetonContext())
                {
                    foreach (var data in ClientsData)
                    {
                        if (!DataValidation.IsValidPhoneNumber(data.Phone))
                        {
                            MessageBox.Show($"Неверный формат телефона: {data.Phone}");
                            LoadData();
                            return; // Пропускаем текущую итерацию цикла
                        }

                        if (!DataValidation.IsValidEmail(data.Email))
                        {
                            MessageBox.Show($"Неверный формат электронной почты: {data.Email}");
                            LoadData();
                            return; // Пропускаем текущую итерацию цикла
                        }

                        var entity = context.Clients.Find(data.ClientId);
                        if (entity != null)
                        {
                            entity.ClientId = data.ClientId;
                            entity.FirstName = data.FirstName;
                            entity.LastName = data.LastName;
                            entity.MiddleName = data.MiddleName;
                            entity.Login = data.Login;
                            entity.Password = data.Password;
                            entity.Phone = data.Phone;
                            entity.Address = data.Address;
                            entity.Email = data.Email;
                        }
                    }
                    context.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
