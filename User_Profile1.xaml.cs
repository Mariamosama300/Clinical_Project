using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Medical_Form
{



    public partial class MainWindow : Window
    {
        string contact_Way;
        string recommended;

        // Create a list of strings
        List<string> selectedContactWays = new List<string>();
        List<string> selectedRecommended = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the values from your UI controls
            string Mname = txtName.Text;
            string email = email1.Text;
            string address = adderesse.Text;
            string phone = phone1.Text;
            string gender = cmbGender.Text;

            string Bdate = dpDateOfBirth.SelectedDate?.ToString("yyyy-MM-dd");
             string job = job1.Text;
             string Hphone = hometel.Text;
             string last_treatment = treatment.Text;


            if (string.IsNullOrEmpty(Mname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) || 
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(Bdate)  || 
                string.IsNullOrEmpty(job) || string.IsNullOrEmpty(Hphone) || string.IsNullOrEmpty(last_treatment))
            { 
                MessageBox.Show("Please Enter All Inputs required!");
                return;
            }

            // Recommended
            if(chkFacebook.IsChecked == true)
            {
                recommended = chkFacebook.Content.ToString();
                AddrecommendedToList(recommended);
            }

            if (chkFriend.IsChecked == true)
            {
                recommended = chkFriend.Content.ToString();
                AddrecommendedToList(recommended);
            }

            if (chkwebsite.IsChecked == true)
            {
                recommended = chkwebsite.Content.ToString();
                AddrecommendedToList(recommended);
            }

            if (chkinsurance.IsChecked == true)
            {
                recommended = chkinsurance.Content.ToString();
                AddrecommendedToList(recommended);
            }

            if (chkother.IsChecked == true)
            {
                recommended = chkother.Content.ToString();
                AddrecommendedToList(recommended);
            }

            if (chksign.IsChecked == true)
            {
                recommended = chksign.Content.ToString();
                AddrecommendedToList(recommended);
            }

            if (chkdoctor.IsChecked == true)
            {
                recommended = chkdoctor.Content.ToString();
                AddrecommendedToList(recommended);
            }

            //Contact Way
            if (chkPhone.IsChecked == true)
            {
                contact_Way = chkPhone.Content.ToString();
                Addcontact_WayToList(recommended);
            }

            if (chkSms.IsChecked == true)
            {
                contact_Way = chkSms.Content.ToString();
                Addcontact_WayToList(recommended);
            }

            if (chkWhatsapp.IsChecked == true)
            {
                contact_Way = chkWhatsapp.Content.ToString();
                Addcontact_WayToList(recommended);
            }

            contact_Way = string.Join(", ", selectedContactWays);
            recommended = string.Join(", ", selectedRecommended);

            MedicalFormData formData = new MedicalFormData
            {
                Mname = Mname,
                Email = email,
                Address = address,
                Phone = phone,
                Gender = gender,
                Bdate = Bdate,
                Job = job,
                Hphone = Hphone,
                LastTreatment = last_treatment,
                Contact_Way = contact_Way,
                Recommended = recommended,
            };

            userprofile2 userProfile2Window = new userprofile2(formData);
            userProfile2Window.Show();
            this.Close();
        }


        private void Addcontact_WayToList(string selectedTime)
        {
            selectedContactWays.Add(selectedTime);
        }

        private void AddrecommendedToList(string selectedTime)
        {
            selectedRecommended.Add(selectedTime);
        }

    }

}
