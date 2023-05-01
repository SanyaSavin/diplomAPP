using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using diplomAPP.entities;

namespace diplomAPP
{
    /// <summary>
    /// Логика взаимодействия для uslugaf.xaml
    /// </summary>
    public partial class uslugaf : Window
    {
        public uslugaf()
        {
            InitializeComponent();
            dusluga.ItemsSource = bolnicaDBEntities.GetContext().uslugi.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dusluga.SelectedItem != null)
            {
                var usluga = dusluga.SelectedItem as uslugi;
                var result = MessageBox.Show($"Вы уверены, что хотите удалить услугу {usluga.Name_uslugi}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var context = bolnicaDBEntities.GetContext();
                    context.uslugi.Remove(usluga);
                    context.SaveChanges();
                    dusluga.ItemsSource = context.uslugi.ToList();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var newUsluga = new uslugi
            {
                Name_uslugi = txtName.Text // Пример: Значение поля "Название услуги" хранится в TextBox с именем "txtName"
            };

            // Добавляем новый объект в контекст базы данных
            var context = bolnicaDBEntities.GetContext();
            context.uslugi.Add(newUsluga);

            // Сохраняем изменения в базе данных
            context.SaveChanges();
            MessageBox.Show("Запись успешно добавлена!");
            
            // Обновляем источник данных DataGrid
            dusluga.ItemsSource = context.uslugi.ToList();
            txtName.Text = "";
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = textBox1.Text;
            var context = bolnicaDBEntities.GetContext();
            IEnumerable<uslugi> filteredData = context.uslugi.Where(d => d.Name_uslugi.Contains(searchText));
            dusluga.ItemsSource = filteredData.ToList();
            dusluga.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            viborOkna vo = new viborOkna();
            vo.Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var context = bolnicaDBEntities.GetContext();
            var changedItems = dusluga.Items.OfType<uslugi>().Where(p => context.Entry(p).State != EntityState.Unchanged).ToList();
            foreach (var item in changedItems)
            {
                context.Entry(item).State = item.id_usluga == 0 ? EntityState.Added : EntityState.Modified;
            }
            context.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены.");
            
        }
    }
}
 


