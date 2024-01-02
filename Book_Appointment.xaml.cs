using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Software_Project
{
    public partial class Book_Appointment : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";

        public Book_Appointment()
        {
            InitializeComponent();
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string BookAppointmentQuery = "INSERT INTO appointments (appointment_date, patient_id) " +
                                   "VALUES (@appointment_date, @patient_id)";

                    using (MySqlCommand BookAppointmentCmd = new MySqlCommand(BookAppointmentQuery, connection))
                    {
                        BookAppointmentCmd.Parameters.AddWithValue("@patient_id", txtMedicalRecord.Text);
                        BookAppointmentCmd.Parameters.AddWithValue("@appointment_date", dpAppointmentDate.SelectedDate);

                        BookAppointmentCmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }
    }
}