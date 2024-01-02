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
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Navigation;



namespace Medical_Form

{
    
    public partial class userprofile2 : Window
    {
        private const string ConnectionString = "Server=localhost;Database=PROJECT1;User=root;Password=14107;";
        bool radioempty = false;
        private MedicalFormData formData;

        string[] statusValues = { "Yes", "No" };

        public userprofile2(MedicalFormData formData)
        {
            InitializeComponent();
            this.formData = formData;
        }
        private void IsAnyRadioButtonEmpty(Grid grid)
        {
            if ((rdbAttendingYes.IsChecked == false && rdbAttendingNo.IsChecked == false) || (rdbTakinMedicineYes.IsChecked == false && rdbTakinMedicineNo.IsChecked == false) ||
                (rdbAllergicYes.IsChecked == false && rdbAllergicNo.IsChecked == false) || (rdbRheumaticYes.IsChecked == false && rdbRheumaticNo.IsChecked == false) ||
                (rdbjaundiceYes.IsChecked == false && rdbjaundiceNo.IsChecked == false) || (rdbHeartProblemsYes.IsChecked == false && rdbHeartProblemsNo.IsChecked == false) ||
                (rdbAnaethisticYes.IsChecked == false && rdbAnaethisticNo.IsChecked == false) || (rdbPeacemakerYes.IsChecked == false && rdbPeacemakerNo.IsChecked == false) ||
                (rdbBronchitisYes.IsChecked == false && rdbBronchitisNo.IsChecked == false) || (rdbFaintingAttackYes.IsChecked == false && rdbFaintingAttackNo.IsChecked == false) ||
                (rdbDiabetesYes.IsChecked == false && rdbDiabetesNo.IsChecked == false) || (rdbBruiseYes.IsChecked == false && rdbBruiseNo.IsChecked == false) ||
                (rdbSmokeYes.IsChecked == false && rdbSmokeNo.IsChecked == false))
            {
                radioempty = true;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (radioempty)
                {
                    MessageBox.Show("Please answer all questions.");
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string MedicalFormQuery = "INSERT INTO MEDICAL_FORM (Mname, email, address, phone, gender, Bdate, job, Hphone, last_treatment, contact_Way, recommended, attending_treatment, taking_medicines, allergic, fever, disease, heart_problems, bad_reaction, heart_surgery, chest_condition, blackouts, diabetes, bruise, smoke) " +
                                   "VALUES (@Mname, @email, @address, @phone, @gender, @Bdate, @job, @Hphone, @last_treatment, @contact_Way, @recommended, @attending_treatment, @taking_medicines, @allergic, @fever, @disease, @heart_problems, @bad_reaction, @heart_surgery, @chest_condition, @blackouts, @diabetes, @bruise, @smoke)";
                    using (MySqlCommand MedicalFormCmd = new MySqlCommand(MedicalFormQuery, connection))
                    {

                        // Set parameters for userprofile1 data
                        MedicalFormCmd.Parameters.AddWithValue("@Mname", formData.Mname);
                        MedicalFormCmd.Parameters.AddWithValue("@email", formData.Email);
                        MedicalFormCmd.Parameters.AddWithValue("@address", formData.Address);
                        MedicalFormCmd.Parameters.AddWithValue("@phone", formData.Phone);
                        MedicalFormCmd.Parameters.AddWithValue("@gender", formData.Gender);
                        MedicalFormCmd.Parameters.AddWithValue("@Bdate", formData.Bdate);
                        MedicalFormCmd.Parameters.AddWithValue("@job", formData.Job);
                        MedicalFormCmd.Parameters.AddWithValue("@Hphone", formData.Hphone);
                        MedicalFormCmd.Parameters.AddWithValue("@last_treatment", formData.LastTreatment);
                        MedicalFormCmd.Parameters.AddWithValue("@contact_Way", formData.Contact_Way);
                        MedicalFormCmd.Parameters.AddWithValue("@recommended", formData.Recommended);


                        if (rdbAttendingYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@attending_treatment", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@attending_treatment", statusValues[1]); }

                        if (rdbTakinMedicineYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@taking_medicines", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@taking_medicines", statusValues[1]); }

                        if (rdbAllergicYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@allergic", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@allergic", statusValues[1]); }

                        if (rdbRheumaticYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@fever", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@fever", statusValues[1]); }

                        if (rdbjaundiceYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@disease", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@disease", statusValues[1]); }

                        if (rdbHeartProblemsYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@heart_problems", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@heart_problems", statusValues[1]); }

                        if (rdbAnaethisticYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@bad_reaction", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@bad_reaction", statusValues[1]); }

                        if (rdbPeacemakerYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@heart_surgery", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@heart_surgery", statusValues[1]); }

                        if (rdbBronchitisYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@chest_condition", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@chest_condition", statusValues[1]); }

                        if (rdbFaintingAttackYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@blackouts", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@blackouts", statusValues[1]); }

                        if (rdbDiabetesYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@diabetes", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@diabetes", statusValues[1]); }

                        if (rdbBruiseYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@bruise", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@bruise", statusValues[1]); }

                        if (rdbSmokeYes.IsChecked == true)
                        {
                            MedicalFormCmd.Parameters.AddWithValue("@smoke", statusValues[0]);
                        }
                        else { MedicalFormCmd.Parameters.AddWithValue("@smoke", statusValues[1]); }

                        MedicalFormCmd.ExecuteNonQuery();

                        MessageBox.Show("answers are saved");

                    }
                }

                MessageBox.Show("Data from userprofile1 and userprofile2 are saved");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }
    }
}
