using MySql.Data.MySqlClient;
using Software_Project;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Software_Project
{
    public partial class Admin_Search_Edit : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";
        private DataTable dataTable = new DataTable();

        public Admin_Search_Edit()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Admin_Delete delete2Window = new Admin_Delete();
            delete2Window.Show();
            this.Close();
        }
        private void signout(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Save changes before opening the edit window
            UpdateChangesToDatabase();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Admin add = new Admin();
            add.Show();
            this.Close();
        }

        private void LoadData(string searchText, string selectedFilter)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    if (filterComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a filter.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string sanitizedFilter = SanitizeColumnName(selectedFilter);

                    if (!string.IsNullOrEmpty(sanitizedFilter))
                    {
                        string query = $"SELECT Employee_Id, email, Name, Gender, Password, National_ID, Birthday, Education, Phone_Number FROM Employee WHERE {sanitizedFilter} LIKE @searchText";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                dataGrid.ItemsSource = dataTable.DefaultView;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string SanitizeColumnName(string columnName)
        {
            return columnName.Replace(" ", "");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.Trim();
            string selectedFilter = filterComboBox.SelectedItem as ComboBoxItem != null ? GetFilterColumnName(filterComboBox.SelectedItem) : "";

            if (!string.IsNullOrEmpty(selectedFilter))
            {
                LoadData(searchText, selectedFilter);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Call a method to update changes to the database
            UpdateChangesToDatabase();
        }

        private void UpdateChangesToDatabase()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM Employee", connection))
                    {
                        MySqlCommand selectCommand = new MySqlCommand($"SELECT * FROM Employee", connection);
                        adapter.SelectCommand = selectCommand;

                        MySqlCommand updateCommand = new MySqlCommandBuilder(adapter).GetUpdateCommand();



                        adapter.UpdateCommand = updateCommand;
                        adapter.Update(dataTable);
                    }

                    MessageBox.Show("Changes saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int rowIndex = e.Row.GetIndex();

            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string editedValue = ((TextBox)e.EditingElement).Text;
                dataTable.Rows[rowIndex][columnName] = editedValue;
            }

            // Automatically save changes to the database
            UpdateChangesToDatabase();
        }

        private void DataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            // Allow editing for all cells
            e.EditingElement.Focus();
        }

        private string GetFilterColumnName(object selectedItem)
        {
            if (selectedItem is ComboBoxItem comboBoxItem)
            {
                string content = comboBoxItem.Content.ToString();

                switch (content)
                {
                    case "Employee Id":
                        return "Employee_Id";
                    case "Email":
                        return "email";
                    case "Name":
                        return "Name";
                    default:
                        return string.Empty;
                }
            }

            return string.Empty;
        }
    }
}