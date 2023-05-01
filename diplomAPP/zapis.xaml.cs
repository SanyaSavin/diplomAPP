using diplomAPP.entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для zapis.xaml
    /// </summary>
    public partial class zapis : Window
    {
        public zapis()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            using (var context = new bolnicaDBEntities())
            {
                var data = context.uslugi.ToList();
                comboBox1.ItemsSource = data;
                comboBox1.DisplayMemberPath = "Name_uslugi";

                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            menu mu = new menu();
            mu.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fioTX.Text) || string.IsNullOrEmpty(polTX.Text) || string.IsNullOrEmpty(pasTX.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=bolnicaDB;Trusted_Connection=True;");
                conn.Open();
                string query = "INSERT INTO priem (fio, polis, pasport, Name_uslugi) VALUES (@value1, @value2, @value3, @value4)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@value1", fioTX.Text);
                cmd.Parameters.AddWithValue("@value2", polTX.Text);
                cmd.Parameters.AddWithValue("@value3", pasTX.Text);
                cmd.Parameters.AddWithValue("@value4", comboBox1.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Запись оформлена!");
                uspehZapis mu = new uspehZapis();
                mu.Show();
                Close();
            }

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
