// Import the necessary namespaces
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Software_Project
{
    public partial class mainwindow : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";

        public mainwindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = userTextBox.Text;
            string password = PasswordTextBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    // Use a parameterized query to prevent SQL injection
                    string query = "SELECT * FROM Employee WHERE email = @Email AND password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Successful login

                                // Check if '@' is followed by 'example.com'
                                int atIndex = email.IndexOf('@');
                                if (atIndex != -1 && atIndex < email.Length - 1)
                                {
                                    string characterAfterAt = email.Substring(atIndex + 1);

                                    if (characterAfterAt.Equals("doc.com", StringComparison.OrdinalIgnoreCase))
                                    {
                                        MessageBox.Show("Login successful!");
                                        doctormain doctormain = new doctormain();

                                        Window parentWindow = Window.GetWindow(this);
                                        if (parentWindow != null)
                                        {
                                            parentWindow.Close();
                                        }

                                        // Show the new window
                                        doctormain.Show();


                                    }

                                    else if (characterAfterAt.Equals("rec.com", StringComparison.OrdinalIgnoreCase))
                                    {
                                        MessageBox.Show("Login successful!");
                                        Reception receptionmain = new Reception();
                                        Window parentWindow = Window.GetWindow(this);
                                        if (parentWindow != null)
                                        {
                                            parentWindow.Close();
                                        }

                                        // Show the new window
                                        receptionmain.Show();

                                    }
                                    else
                                    {
                                        MessageBox.Show($"Login successful!");
                                        // Create an instance of Window1 and show it
                                        Admin window1 = new Admin();

                                        // Close the current window (assuming it's the login page)
                                        Window parentWindow = Window.GetWindow(this);
                                        if (parentWindow != null)
                                        {
                                            parentWindow.Close();
                                        }

                                        // Show the new window
                                        window1.Show();

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Login Failed!");
                                }


                            }
                            else
                            {
                                // Failed login
                                MessageBox.Show("Invalid email or password.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


    }
}