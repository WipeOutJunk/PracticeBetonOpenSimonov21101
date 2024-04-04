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
    /// Логика взаимодействия для qualitiControl.xaml
    /// </summary>
    public partial class qualitiControl : Page
    {
        private ObservableCollection<Qualitycontrol> QualityControlsData = new ObservableCollection<Qualitycontrol>();

        public qualitiControl(int userRole)
        {
            InitializeComponent();
            if (userRole == 2) // '2' здесь используется в качестве примера для "readonly"
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


        // Этот метод вызывается, когда страница загружается
        private void LoadData()
        {
            try
            {
                using (var context = new PracticeBetonContext())
                {
                    var data = context.Qualitycontrols.OrderBy(qc => qc.QcId).ToList();
                    QualityControlsData.Clear();
                    foreach (var item in data)
                    {
                        QualityControlsData.Add(item);
                    }
                }
                dataGrid.ItemsSource = QualityControlsData;
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
                    foreach (var qc in QualityControlsData)
                    {
                        if (qc.QcId == 0) // Предполагая, что новые записи имеют qc_id равный 0
                        {
                            context.Qualitycontrols.Add(qc);
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
                var selectedItems = dataGrid.SelectedItems.Cast<Qualitycontrol>().ToList();

                if (selectedItems.Any())
                {
                    using (var context = new PracticeBetonContext())
                    {
                        foreach (var qc in selectedItems)
                        {
                            var entity = context.Qualitycontrols.Find(qc.QcId);
                            if (entity != null)
                            {
                                context.Qualitycontrols.Remove(entity);
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
                    foreach (var qc in QualityControlsData)
                    {
                        var entity = context.Qualitycontrols.Find(qc.QcId);
                        if (entity != null)
                        {
                            entity.ProductId = qc.ProductId;
                            entity.CheckDate = qc.CheckDate;
                            entity.Result = qc.Result;
                            entity.Notes = qc.Notes;
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
