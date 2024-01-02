using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Log_In
{
    /// <summary>
    /// Interaction logic for doctormain.xaml
    /// </summary>
    public partial class doctormain : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";

        public ObservableCollection<YourDataModel> DataList { get; set; }

        public doctormain()
        {
            InitializeComponent();
            DataList = new ObservableCollection<YourDataModel>();
            LoadDataFromDatabase();
        }
        private void ToolsButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the other window
           // material otherWindow = new material();

            // Show the other window
           // otherWindow.Show();

            // Optionally, close or hide the current window
            //this.Close();
        }
        private void calender_click(object sender, RoutedEventArgs e)
        {
            //trymaking otherWindow = new trymaking();
             //chatgptprescription chatgptprescription = new chatgptprescription();
              //chatgptprescription.Show();

             //otherWindow.Show();
            // Optionally, close or hide the current window

            //ocrwindow ocr = new ocrwindow();    
           // ocr.Show();
            //this.Close();

        }
        private void signout(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            // Show the other window
           //window1.Show();

            // Optionally, close or hide the current window
            this.Close();

        }
        private void news_Click(object sender, RoutedEventArgs e) {
            //newsevent otherWindow = new newsevent();

            // Show the other window
           // otherWindow.Show();

            // Optionally, close or hide the current window
            //this.Close();
            

                 }
        private void analytic_click(object sender, RoutedEventArgs e)
        {
           // patientviewf patientview = new patientviewf();
           // patientview.Show();
           // this.Close();
        }



        private void LoadDataFromDatabase()
        {
            DataList.Clear(); // Clear existing data if needed

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM appointments";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            YourDataModel data = new YourDataModel
                            {
                                appointment_id = reader["appointment_id"].ToString(),
                                doctor_id = reader["doctor_id"].ToString(),
                                patient_id = reader["patient_id"].ToString(),
                                appointment_date = reader["appointment_date"].ToString(),
                                // Set other properties accordingly
                            };
                            DataList.Add(data);
                        }
                    }
                }
            }

            // Bind the data to the DataGrid
            YourDataGrid.ItemsSource = DataList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}