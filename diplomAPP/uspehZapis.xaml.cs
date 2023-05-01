using System;
using diplomAPP.entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using ZXing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ZXing.PDF417.Internal;

namespace diplomAPP
{
    /// <summary>
    /// Логика взаимодействия для uspehZapis.xaml
    /// </summary>
    public partial class uspehZapis : Window
    {
        public uspehZapis()
        {
            InitializeComponent();
            string filePath = "barcode.png";
            string barcodeValue = "";
            using (SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS;Database=bolnicaDB;Trusted_Connection=True;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id_priem FROM priem", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        barcodeValue = reader.GetInt32(0).ToString();
                    }
                }
            }

            // Создаем объект штрих-кода
            BarcodeWriter barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128 // Выбираем формат штрих-кода
            };

            // Генерируем изображение штрих-кода на основе значения из базы данных
            Bitmap barcodeBitmap = barcodeWriter.Write(barcodeValue);

            // Сохраняем изображение штрих-кода в файл
            barcodeBitmap.Save(filePath, ImageFormat.Png);

            // Отображаем изображение штрих-кода в приложении WPF
            // Здесь необходимо заменить "imageControl" на имя элемента управления Image, на котором должно быть отображено изображение штрих-кода
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                imageControl.Source = image;
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string filePath = "barcode.png";
            string barcodeValue = "";
            using (SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS;Database=bolnicaDB;Trusted_Connection=True;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id_priem FROM priem", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        barcodeValue = reader.GetInt32(0).ToString();
                    }
                }
            }

            // Создаем объект штрих-кода
            BarcodeWriter barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128 // Выбираем формат штрих-кода
            };

            // Генерируем изображение штрих-кода на основе значения из базы данных
            Bitmap barcodeBitmap = barcodeWriter.Write(barcodeValue);

            // Сохраняем изображение штрих-кода в файл
            barcodeBitmap.Save(filePath, ImageFormat.Png);

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "PDF documents (.pdf)|.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Создаем документ PDF
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));

                document.Open();

                // Добавляем изображение штрих-кода в документ
                iTextSharp.text.Image barcodeImage = iTextSharp.text.Image.GetInstance(filePath);
                document.Add(barcodeImage);

                document.Close();
            }
        }
    }
}
