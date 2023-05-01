using diplomAPP.entities;
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

namespace diplomAPP
{
    /// <summary>
    /// Логика взаимодействия для sotrF.xaml
    /// </summary>
    public partial class sotrF : Window
    {
        public sotrF()
        {
            InitializeComponent();
            dpriem.ItemsSource = bolnicaDBEntities.GetContext().sotrudniki.ToList();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = textBox1.Text;
            var context = bolnicaDBEntities.GetContext();
            IEnumerable<sotrudniki> filteredData = context.sotrudniki.Where(d =>
    d.fio.Contains(searchText) ||
    d.adres.Contains(searchText) ||
    d.dolznost.Contains(searchText) ||
    d.zarplata.Contains(searchText));

            dpriem.ItemsSource = filteredData.ToList();
            dpriem.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dpriem.SelectedItem != null)
            {
                var usluga = dpriem.SelectedItem as sotrudniki;
                var result = MessageBox.Show($"Вы уверены, что хотите удалить услугу?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var context = bolnicaDBEntities.GetContext();
                    context.sotrudniki.Remove(usluga);
                    context.SaveChanges();
                    dpriem.ItemsSource = context.sotrudniki.ToList();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var priem = new sotrudniki
            {
                fio = txtName.Text,
                adres = txtName1.Text,
                dolznost = txtName2.Text,
                zarplata = txtName3.Text
            };

            // Добавляем новый объект в контекст базы данных
            var context = bolnicaDBEntities.GetContext();
            context.sotrudniki.Add(priem);

            // Сохраняем изменения в базе данных
            context.SaveChanges();
            MessageBox.Show("Запись успешно добавлена!");

            // Обновляем источник данных DataGrid
            dpriem.ItemsSource = context.sotrudniki.ToList();
            txtName.Text = "";
            txtName1.Text = "";
            txtName2.Text = "";
            txtName3.Text = "";
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
            var changedItems = dpriem.Items.OfType<sotrudniki>().Where(p => context.Entry(p).State != EntityState.Unchanged).ToList();
            foreach (var item in changedItems)
            {
                context.Entry(item).State = item.id_sot == 0 ? EntityState.Added : EntityState.Modified;
            }
            context.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены.");
        }
    }
}
