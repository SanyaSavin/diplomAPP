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
using diplomAPP.entities;

namespace diplomAPP
{
    /// <summary>
    /// Логика взаимодействия для priemf.xaml
    /// </summary>
    public partial class priemf : Window
    {
        public priemf()
        {
            InitializeComponent();
            dpriem.ItemsSource = bolnicaDBEntities.GetContext().priem.ToList();

            using (var context = new bolnicaDBEntities())
            {
                var data = context.uslugi.ToList();
                comboBox1.ItemsSource = data;
                comboBox1.DisplayMemberPath = "Name_uslugi";


            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = textBox1.Text;
            var context = bolnicaDBEntities.GetContext();
            IEnumerable<priem> filteredData = context.priem.Where(d =>
    d.fio.Contains(searchText) ||
    d.polis.Contains(searchText) ||
    d.pasport.Contains(searchText) ||
    d.Name_uslugi.Contains(searchText));

            dpriem.ItemsSource = filteredData.ToList();
            dpriem.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var priem = new priem
            {
                fio = txtName.Text,
                polis = txtName1.Text,
                pasport = txtName2.Text,
                Name_uslugi = comboBox1.Text
            };

            // Добавляем новый объект в контекст базы данных
            var context = bolnicaDBEntities.GetContext();
            context.priem.Add(priem);

            // Сохраняем изменения в базе данных
            context.SaveChanges();
            MessageBox.Show("Запись успешно добавлена!");

            // Обновляем источник данных DataGrid
            dpriem.ItemsSource = context.priem.ToList();
            txtName.Text = "";
            txtName1.Text = "";
            txtName2.Text = "";
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dpriem.SelectedItem != null)
            {
                var usluga = dpriem.SelectedItem as priem;
                var result = MessageBox.Show($"Вы уверены, что хотите удалить услугу?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var context = bolnicaDBEntities.GetContext();
                    context.priem.Remove(usluga);
                    context.SaveChanges();
                    dpriem.ItemsSource = context.priem.ToList();
                }
            }
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
            var changedItems = dpriem.Items.OfType<priem>().Where(p => context.Entry(p).State != EntityState.Unchanged).ToList();
            foreach (var item in changedItems)
            {
                context.Entry(item).State = item.id_priem == 0 ? EntityState.Added : EntityState.Modified;
            }
            context.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены.");
        }

        
    }
}
