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
    /// Логика взаимодействия для AuthSotr.xaml
    /// </summary>
    public partial class AuthSotr : Window
    {
        public AuthSotr()
        {
            InitializeComponent();
            String allowchar = " ";

            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";

            allowchar += "1,2,3,4,5,6,7,8,9,0";

            char[] a = { ',' };

            String[] ar = allowchar.Split(a);

            String pwd = " ";

            string temp = " ";

            Random r = new Random();



            for (int i = 0; i < 6; i++)

            {

                temp = ar[(r.Next(0, ar.Length))];



                pwd += temp;

            }



            textBox1.Text = pwd;
            textBox1.Text = textBox1.Text.Trim();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=bolnicaDB;Trusted_Connection=True;");
            SqlCommand cmd = new SqlCommand("SELECT login, password FROM loginpass WHERE login=@login AND password=@password", conn);
            cmd.Parameters.AddWithValue("@login", usernameTextBox.Text);
            cmd.Parameters.AddWithValue("@password", passwordTextBox.Password);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() && textBox1.Text == textBox2.Text)
            {
                MessageBox.Show("Вы успешно вошли!");
                viborOkna m = new viborOkna();
                m.Show();
                Close();

            }
            else
            {
                MessageBox.Show("Неправильный логин, пароль или капча");
            }
            conn.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String allowchar = " ";

            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";

            allowchar += "1,2,3,4,5,6,7,8,9,0";

            char[] a = { ',' };

            String[] ar = allowchar.Split(a);

            String pwd = " ";

            string temp = " ";

            Random r = new Random();



            for (int i = 0; i < 6; i++)

            {

                temp = ar[(r.Next(0, ar.Length))];



                pwd += temp;

            }



            textBox1.Text = pwd;
            textBox1.Text = textBox1.Text.Trim();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == textBox2.Text)
            {
                MessageBox.Show("Капча введена верно");
            }
            else
            {
                MessageBox.Show("Капча введена неверно");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            menu menu = new menu();
            menu.Show();
            Close();
        }
    }

}
    

