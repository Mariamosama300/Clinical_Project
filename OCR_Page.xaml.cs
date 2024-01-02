using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Ocsp;
using System;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Tesseract;

namespace Software_Project
{
    public partial class OCR_Page : Window
    {
        string imagepath = @"C:\\Users\\7mada\\Documents\\OCR\\OCR\\img\\pic6.jpg";
        public OCR_Page()
        {
            InitializeComponent();
            //imageControl.Source = new BitmapImage(new Uri(imagepath));
        }

        private void PerformOcrButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var engine = new TesseractEngine("./tessData", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagepath))
                    {
                        using (var page = engine.Process(img))
                        {
                            // Get the recognized text
                            NewTextBlock.Text = page.GetText();
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }
    }
}