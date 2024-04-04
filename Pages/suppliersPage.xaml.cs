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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeBetonNetV.Pages
{
    /// <summary>
    /// Логика взаимодействия для suppliersPage.xaml
    /// </summary>
    public partial class suppliersPage : Page
    {
        private ObservableCollection<Supplier> SuppliersData = new ObservableCollection<Supplier>();
        public suppliersPage(int userRole)
        {
            InitializeComponent();
            if (userRole == 6) // '2' здесь используется в качестве примера для "readonly"
            {
                // Скрываем панель с кнопками
                ActionButtonsPanel.Visibility = Visibility.Collapsed;

                // Устанавливаем DataGrid в режим только для чтения
                dataGrid.IsReadOnly = true;

                // Отключаем возможность добавления новых строк
                dataGrid.CanUserAddRows = false;
            }
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (var context = new PracticeBetonContext())
                {
                    var data = context.Suppliers.OrderBy(id => id.SupplierId).ToList();
                    SuppliersData.Clear();
                    foreach (var item in data)
                    {
                        SuppliersData.Add(item);
                    }
                }
                dataGrid.ItemsSource = SuppliersData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = dataGrid.SelectedItems.Cast<Supplier>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new PracticeBetonContext())
                    {
                        foreach (var data in selectedItems)
                        {
                            var entity = context.Suppliers.Find(data.SupplierId);
                            if (entity != null)
                            {
                                context.Suppliers.Remove(entity);
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new PracticeBetonContext())
                {
                    foreach (var entity in SuppliersData)
                    {
                        if (entity.SupplierId == 0) // Предполагая, что новые записи имеют qc_id равный 0
                        {
                            if (!DataValidation.IsValidPhoneNumber(entity.Phone))
                            {
                                MessageBox.Show($"Неверный формат телефона: {entity.Phone}");
                                LoadData();
                                return; // Пропускаем текущую итерацию цикла
                            }

                            if (!DataValidation.IsValidEmail(entity.Email))
                            {
                                MessageBox.Show($"Неверный формат электронной почты: {entity.Email}");
                                LoadData();
                                return; // Пропускаем текущую итерацию цикла
                            }
                            context.Suppliers.Add(entity);
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new PracticeBetonContext())
                {
                    foreach (var data in SuppliersData)
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
                        var entity = context.Suppliers.Find(data.SupplierId);
                        if (entity != null)
                        {
                            entity.SupplierId = data.SupplierId;
                            entity.Name = data.Name;
                            entity.Address = data.Address;
                            entity.Email = data.Email;
                            entity.Phone = data.Phone;
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
