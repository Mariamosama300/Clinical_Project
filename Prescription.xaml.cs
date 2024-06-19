using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Software_Project
{
    public partial class Prescription : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";

        public Prescription()
        {
            InitializeComponent();
        }

        // Create a variable for last prescription id to add drugs from it
        private int lastPrescriptionId;
        string Dosage;
        string days;
        string timehours;

        // Create a list of strings
        List<string> drugList = new List<string>();
        List<string> diagnosisList = new List<string>();
        List<string> timehoursList = new List<string>();

        // Dictionaries
        private Dictionary<string, List<string>> drugInteractions;
        private Dictionary<string, string> phenotypeRecommendations;

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Insert data into the database
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string diagnoses_names = string.Join(", ", diagnosisList);

                    string PrescriptionQuery = "INSERT INTO Prescriptions (patient_name, diagnosis, note) " +
                                   "VALUES (@patient_name, @diagnosis, @note)";

                    using (MySqlCommand PrescriptionCmd = new MySqlCommand(PrescriptionQuery, connection))
                    {
                        PrescriptionCmd.Parameters.AddWithValue("@patient_name", txtPatientName.Text);
                        PrescriptionCmd.Parameters.AddWithValue("@diagnosis", diagnoses_names);
                        PrescriptionCmd.Parameters.AddWithValue("@note", txtNotes.Text);

                        PrescriptionCmd.ExecuteNonQuery();
                    }

                    //Dosage
                    if (rdbOnce.IsChecked == true)
                    {
                        Dosage = rdbOnce.Content.ToString();
                    }
                    else if (rdbTwice.IsChecked == true)
                    {
                        Dosage = rdbTwice.Content.ToString();
                    }
                    else if (rdbThrice.IsChecked == true)
                    {
                        Dosage = rdbThrice.Content.ToString();
                    }
                    else if (rdbQuarter.IsChecked == true)
                    {
                        Dosage = rdbQuarter.Content.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Please select dosage before adding.");
                        return;
                    }

                    //Days
                    if (rdbEveryDay.IsChecked == true)
                    {
                        days = rdbEveryDay.Content.ToString();
                    }
                    else if (rdbEveryOtherDay.IsChecked == true)
                    {
                        days = rdbEveryOtherDay.Content.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Please select days before submitting.");
                        return;
                    }

                    //Time Hours
                    if (chbDay.IsChecked == true)
                    {
                        timehours = chbDay.Content.ToString();
                        AddTimeHoursToList(timehours);
                    }
                    if (chbNight.IsChecked == true)
                    {
                        timehours = chbNight.Content.ToString();
                        AddTimeHoursToList(timehours);
                    }
                    if (chbBeforeEating.IsChecked == true)
                    {
                        timehours = chbBeforeEating.Content.ToString();
                        AddTimeHoursToList(timehours);
                    }
                    if (chbAfterEating.IsChecked == true)
                    {
                        timehours = chbAfterEating.Content.ToString();
                        AddTimeHoursToList(timehours);
                    }
                    if ((chbDay.IsChecked != true) && (chbNight.IsChecked != true) &&
                        (chbBeforeEating.IsChecked != true) && (chbAfterEating.IsChecked != true))
                    {
                        MessageBox.Show("Please select time before submitting.");
                        return;
                    }

                    // Retrieve the last inserted user ID
                    string getLastPrescriptionIdQuery = "SELECT LAST_INSERT_ID()";

                    using (MySqlCommand getLastPresriptionIdCmd = new MySqlCommand(getLastPrescriptionIdQuery, connection))
                    {
                        lastPrescriptionId = Convert.ToInt32(getLastPresriptionIdCmd.ExecuteScalar());
                    }
                }
                AddDrugToDatabase(drugList, timehoursList, Dosage, days);
                MessageBox.Show("Data saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void btnAddDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Code to execute when the "Add diagnosis" button is clicked

                // Get the selected diagnosis from the ComboBox
                string selectedDiagnosis = cmbDiagnosis.Text;

                // Check if a diagnosis is selected
                if (!string.IsNullOrEmpty(selectedDiagnosis))
                {
                    // Call a method to add the selected diagnosis to the database
                    AddDiagnosisToList(selectedDiagnosis);

                    // Optionally, you can perform additional actions or update UI elements here
                    // ...
                    MessageBox.Show("Diagnosis added successfully!");
                }
                else
                {
                    MessageBox.Show("Please select a diagnosis before adding.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding diagnosis: {ex.Message}");
            }
        }

        private void AddDiagnosisToList(string selectedDiagnosis)
        {
            diagnosisList.Add(selectedDiagnosis);
        }

        private void btnAddDrug_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClinicalDecisionSupport();

                // Code to execute when the "Add Drug" button is clicked

                // Get the selected drug from the ComboBox
                string selectedDrug = cmbDrug.Text;

                // Check if a drug is selected
                if (!string.IsNullOrEmpty(selectedDrug))
                {
                    foreach (string drug in drugList)
                    {
                        CheckDrugInteraction(selectedDrug, drug);
                    }

                    CheckPhenotypeRecommendation(selectedDrug);

                    // Call a method to add the selected drug to the database
                    drugList.Add(selectedDrug);

                    // Optionally, you can perform additional actions or update UI elements here
                    // ...
                    MessageBox.Show("Drug added successfully!");
                    cmbDrug.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Please select a drug before adding.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding drug: {ex.Message}");
            }
        }

        private void AddDrugToDatabase(List<string> selectedDrugs, List<string> selectedTimehours, string Dosage, string days)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string drug_names = string.Join(", ", selectedDrugs);
                    string timehoursjoin = string.Join(", ", selectedTimehours);

                    string DrugsQuery = "INSERT INTO Drugs (drug_name, prescription_id, time_hours, until_date, dosage, days)" +
                        "VALUES (@drug_name, @prescription_id, @time_hours, @until_date, @dosage, @days)";

                    using (MySqlCommand DrugsCmd = new MySqlCommand(DrugsQuery, connection))
                    {
                        DrugsCmd.Parameters.AddWithValue("@drug_name", drug_names);
                        DrugsCmd.Parameters.AddWithValue("@prescription_id", lastPrescriptionId);
                        DrugsCmd.Parameters.AddWithValue("@time_hours", timehoursjoin);
                        DrugsCmd.Parameters.AddWithValue("@until_date", dpDateUntil.SelectedDate);
                        DrugsCmd.Parameters.AddWithValue("@dosage", Dosage);
                        DrugsCmd.Parameters.AddWithValue("@days", days);

                        DrugsCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Drug added to the database successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding drug to the database: {ex.Message}");
            }
        }

        private void AddTimeHoursToList(string selectedTime)
        {
            timehoursList.Add(selectedTime);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            doctormain add = new doctormain();
            add.Show();
            this.Close();
        }

        // Clinical Decision Support System (DSS)
        public void ClinicalDecisionSupport()
        {
            drugInteractions = new Dictionary<string, List<string>>
            {
                { "aspirin", new List<string> { "ibuprofen", "naproxen" } },
                { "ibuprofen", new List<string> { "aspirin" } },
                { "acetaminophen", new List<string> { "ibuprofen" } },
                { "bupivacaine liposome", new List<string> { "lidocaine" } },
                { "dofetilide", new List<string> { "lidocaine" } },
                { "eliglustat", new List<string> { "lidocaine" } },
                { "flibanserin", new List<string> { "lidocaine" } },
                { "lomitapide", new List<string> { "lidocaine" } }
            };

            phenotypeRecommendations = new Dictionary<string, string>
            {
                { "aspirin", "Reduced efficacy in poor metabolizers" },
                { "ibuprofen", "Increased risk of gastrointestinal bleeding in fast metabolizers" },
                { "acetaminophen", "Hepatotoxicity risk in slow metabolizers" },
                { "bupivacaine liposome", "Increased duration of action in intermediate metabolizers" },
                { "dofetilide", "QT prolongation risk in poor metabolizers" },
                { "eliglustat", "Reduced efficacy in fast metabolizers" },
                { "flibanserin", "Increased sedation in slow metabolizers" },
                { "lomitapide", "Hepatotoxicity risk in poor metabolizers" }
            };
        }

        public void CheckDrugInteraction(string selectedDrug, string existingDrug)
        {
            if (drugInteractions.ContainsKey(existingDrug) && drugInteractions[existingDrug].Contains(selectedDrug))
            {
                MessageBox.Show($"Warning: Interaction between {selectedDrug} and {existingDrug}.");
            }
        }

        public void CheckPhenotypeRecommendation(string selectedDrug)
        {
            if (phenotypeRecommendations.ContainsKey(selectedDrug))
            {
                MessageBox.Show($"Phenotype Recommendation: {phenotypeRecommendations[selectedDrug]}");
            }
        }
    }
}

