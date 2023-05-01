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
using System.Windows.Shapes;

namespace diplomAPP
{
    /// <summary>
    /// Логика взаимодействия для viborOkna.xaml
    /// </summary>
    public partial class viborOkna : Window
    {
        public viborOkna()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            priemf pf = new priemf();
            pf.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            uslugaf uf = new uslugaf();
            uf.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            sotrF sf = new sotrF();
            sf.Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            menu mu = new menu();
            mu.Show();
            Close();
        }
    }
}
