using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Data;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using Software_Project;
using System.Windows.Controls;

namespace Software_Project
{
    public partial class Admin_Delete : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";

        public Admin_Delete()
        {
            InitializeComponent();
            LoadData();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the delete2 window
            Admin add = new Admin();
            add.Show();
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the search window
            Admin_Search_Edit searchWindow = new Admin_Search_Edit();

            // Show the search window
            searchWindow.Show();

            // Optionally, you can close the current window if needed
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the delete2 window
            Admin_Delete delete2Window = new Admin_Delete();
            delete2Window.Show();
            this.Close();
        }



        private void LoadData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Retrieve specific columns (ID, email, name) from the "Receptionist" and "Employee" tables using JOIN
                    string query = "SELECT Employee_Id, email, Name FROM Employee";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Set the primary key for the DataTable
                        dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Employee_Id"] };

                        // Bind the DataTable to the DataGrid
                        dataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void signout(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the selected row
                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;

                if (selectedRow != null)
                {
                    // Extract necessary information from the selected row
                    string employeeName = selectedRow["Name"].ToString();

                    // Show a confirmation dialog before deleting
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {employeeName}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                        {
                            connection.Open();

                            // Get the Employee_Id from the selected row
                            int employeeId = Convert.ToInt32(selectedRow["Employee_Id"]);

                            // Execute DELETE query
                            string deleteQuery = $"DELETE FROM Employee WHERE Employee_Id = {employeeId}";

                            using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                            {
                                deleteCommand.ExecuteNonQuery();
                            }

                            // Remove the selected row from the DataGrid
                            DataView dataView = (DataView)dataGrid.ItemsSource;
                            DataTable dataTable = dataView.Table; // Get the underlying DataTable
                            DataRow rowToDelete = dataTable.Rows.Find(employeeId); // Find the DataRow in the DataTable using the primary key
                            if (rowToDelete != null)
                            {
                                rowToDelete.Delete(); // Mark the row for deletion
                                dataTable.AcceptChanges(); // Commit the changes
                            }

                            // Refresh the DataGrid
                            dataGrid.ItemsSource = dataTable.DefaultView;
                        }

                        MessageBox.Show($"Employee {employeeName} deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        // ... other methods remain unchanged
    }
}