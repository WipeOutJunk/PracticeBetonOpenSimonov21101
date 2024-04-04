using Microsoft.EntityFrameworkCore;
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
using PracticeBetonNetV.CustomClasses;

namespace PracticeBetonNetV.Pages
{
    /// <summary>
    /// Логика взаимодействия для orderPage.xaml
    /// </summary>
    public partial class orderPage : Page
    {
       
        private ObservableCollection<OrderViewModel> OrderViewModel = new ObservableCollection<OrderViewModel>();

        public orderPage(int userId)
        {
            InitializeComponent();
            if(userId == 8)
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
                    var data = context.Orders
                                      .Include(order => order.Client)
                                      .Include(order => order.Product)
                                      .Select(order => new OrderViewModel
                                      {
                                          OrderId = order.OrderId,
                                          ProductName = order.Product.Name,
                                          ClientAddress = order.Client.Address,
                                          ProductId = order.ProductId,
                                          ClientId = order.ClientId,
                                          Quantity = order.Quantity,
                                          OrderDate = order.OrderDate,
                                          DeliveryDate = order.DeliveryDate,
                                          Status = order.Status
                                      })
                                      .OrderBy(vm => vm.OrderId)
                                      .ToList();

                    OrderViewModel.Clear();
                    foreach (var item in data)
                    {
                        OrderViewModel.Add(item);
                    }
                }
                dataGrid.ItemsSource = OrderViewModel;
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
                    foreach (var data in OrderViewModel)
                    {
                        var newOrder = new Order
                        {
                            ProductId = data.ProductId,
                            ClientId = data.ClientId,
                            Quantity = data.Quantity,
                            OrderDate = data.OrderDate,
                            DeliveryDate = data.DeliveryDate,
                            Status = data.Status
                        };  
                        context.Orders.Add(newOrder);
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
                var selectedItems = dataGrid.SelectedItems.Cast<OrderViewModel>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new PracticeBetonContext())
                    {
                        foreach (var data in selectedItems)
                        {
                            var entity = context.Orders.Find(data.OrderId);
                            if (entity != null)
                            {
                                context.Orders.Remove(entity);
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
                    foreach (var orderVM in OrderViewModel)
                    {
                        // Находим существующую запись заказа
                        var order = context.Orders.Include(o => o.Product).Include(o => o.Client)
                                                  .SingleOrDefault(o => o.OrderId == orderVM.OrderId);
                        if (order != null)
                        {
                            // Обновляем информацию о заказе
                            order.ProductId = orderVM.ProductId;
                            order.ClientId = orderVM.ClientId;
                            order.Quantity = orderVM.Quantity;
                            order.OrderDate = orderVM.OrderDate;
                            order.DeliveryDate = orderVM.DeliveryDate;
                            order.Status = orderVM.Status;

                            //// Обновляем информацию о продукте, если имя продукта было изменено
                            //if (order.Product.Name != orderVM.ProductName)
                            //{
                            //    order.Product.Name = orderVM.ProductName;
                            //}

                            
                            //if (order.Client.Address != orderVM.ClientAddress)
                            //{
                            //    order.Client.Address = orderVM.ClientAddress;
                            //}
                        }
                    }
                    context.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены.");
                LoadData(); // Обновите данные в UI
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
