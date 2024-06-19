using MySql.Data.MySqlClient;
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
    /// Interaction logic for adddoctor.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";

        public Admin()
        {
            InitializeComponent();
        }

        private void signup2_click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    if (DoesEmailOrNationalIdExist(connection, txtEmail.Text, txtNationalId.Text))
                    {
                        MessageBox.Show("Email or National ID already exists. Please use a different email or National ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Exit the method
                    }
                    if (DoesPhoneNumberExist(connection, txtPhoneNumber.Text))
                    {
                        MessageBox.Show("Phone Number already exists. Please use a different Phone Number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Exit the method
                    }


                    string employeeQuery = "INSERT INTO Employee ( email, education, password, phone_number, Gender, National_ID, birthday, Name) " +
                                           "VALUES ( @Email, @Education, @Password, @PhoneNumber, @Gender, @NationalId, @Birthday, @Name)";

                    using (MySqlCommand employeeCmd = new MySqlCommand(employeeQuery, connection))
                    {
                        //employeeCmd.Parameters.AddWithValue("@Employee_Id", lastReceptionistId);
                        employeeCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        employeeCmd.Parameters.AddWithValue("@Education", txtEducation.Text);
                        employeeCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                        employeeCmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                        employeeCmd.Parameters.AddWithValue("@Gender", cmbGender.Text);
                        employeeCmd.Parameters.AddWithValue("@NationalId", txtNationalId.Text);
                        employeeCmd.Parameters.AddWithValue("@Birthday", dpDateOfBirth.SelectedDate);
                        employeeCmd.Parameters.AddWithValue("@Name", $"{txtFirstName.Text} {txtLastName.Text}");


                        employeeCmd.ExecuteNonQuery();
                    }

                    // Retrieve the last inserted employee ID
                    string getLastEmployeeIdQuery = "SELECT LAST_INSERT_ID()";
                    int lastEmployeeId;

                    using (MySqlCommand getLastEmployeeIdCmd = new MySqlCommand(getLastEmployeeIdQuery, connection))
                    {
                        lastEmployeeId = Convert.ToInt32(getLastEmployeeIdCmd.ExecuteScalar());
                    }

                    // Insert data into the "Receptionist" table
                    // Insert data into the "Receptionist" table
                    string DoctorQuery = "INSERT INTO Doctor (Employee_Id, major, experience) " +
                                               "VALUES (@Employee_Id, @major, @experience)";

                    using (MySqlCommand DoctorCmd = new MySqlCommand(DoctorQuery, connection))
                    {
                        DoctorCmd.Parameters.AddWithValue("@Employee_Id", lastEmployeeId);
                        DoctorCmd.Parameters.AddWithValue("@major", txtmajor.Text);
                        DoctorCmd.Parameters.AddWithValue("@experience", txtex.Text);

                        DoctorCmd.ExecuteNonQuery();
                    }


                    // Display success message
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool DoesEmailOrNationalIdExist(MySqlConnection connection, string email, string nationalId)
        {
            string checkExistenceQuery = "SELECT COUNT(*) FROM Employee WHERE email = @Email OR National_ID = @NationalId";


            using (MySqlCommand checkExistenceCommand = new MySqlCommand(checkExistenceQuery, connection))
            {
                checkExistenceCommand.Parameters.AddWithValue("@Email", email);
                checkExistenceCommand.Parameters.AddWithValue("@NationalId", nationalId);

                int existenceCount = Convert.ToInt32(checkExistenceCommand.ExecuteScalar());
                return existenceCount > 0;
            }
        }

        private bool DoesPhoneNumberExist(MySqlConnection connection, string phoneNumber)
        {
            string checkPhoneNumberQuery = "SELECT COUNT(*) FROM Employee WHERE phone_number = @PhoneNumber";

            using (MySqlCommand checkPhoneNumberCommand = new MySqlCommand(checkPhoneNumberQuery, connection))
            {
                checkPhoneNumberCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                int phoneNumberCount = Convert.ToInt32(checkPhoneNumberCommand.ExecuteScalar());
                return phoneNumberCount > 0;
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Admin_Delete window1 = new Admin_Delete();
            // Show the other window
            window1.Show();

            // Optionally, close or hide the current window
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Admin_Search_Edit window1 = new Admin_Search_Edit();
            // Show the other window
            window1.Show();

            // Optionally, close or hide the current window
            this.Close();
        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Handle text box text changed event
        }
    }
}