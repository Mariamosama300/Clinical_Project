using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Tesseract;

namespace Software_Project
{
    public partial class OCR_Page : Window
    {
        string imagepath = string.Empty;

        public OCR_Page()
        {
            InitializeComponent();
        }

        private void PickImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                imagepath = openFileDialog.FileName;
                imageControl.Source = new BitmapImage(new Uri(imagepath));
            }
        }

        private void PerformOcrButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(imagepath))
            {
                MessageBox.Show("Please select an image file first.");
                return;
            }

            try
            {
                using (var engine = new TesseractEngine("./tessData", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagepath))
                    {
                        using (var page = engine.Process(img))
                        {
                            // Get the recognized text
                            ocrTextBlock.Text = page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing OCR: {ex.Message}");
            }
        }

        private void BackToReception_Click(object sender, RoutedEventArgs e)
        {
            Reception receptionPage = new Reception();
            receptionPage.Show();
            Close(); // Close the current OCR_Page window
        }
    }
}
