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

namespace Software_Project
{
    /// <summary>
    /// Interaction logic for Reception.xaml
    /// </summary>
    public partial class Reception : Window
    {
        public Reception()
        {
            InitializeComponent();
        }

        private void Book_Appointment_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Book Appointment window
            Book_Appointment add = new Book_Appointment();
            add.Show();
            this.Close();
        }

        private void OCR_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the OCR window
            OCR_Page add = new OCR_Page();
            add.Show();
            this.Close();
        }

        private void Button_Click(object sender, object e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DoctorSchedule_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClinicalFiles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Discounts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IVRBooking_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToolsMaterials_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChatBot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            mainwindow loginWindow = new mainwindow();
            loginWindow.Show();
            this.Close();
        }

        private void Payments_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TodaysReviews_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Attendees_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateRecords_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
