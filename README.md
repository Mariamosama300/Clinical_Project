Clinical Project
Project Description
This project consists of two applications designed to streamline dental clinic operations and improve patient experience:

C# Desktop Application: A comprehensive dental clinic management system with views for admin, doctor, and reception.
Flutter Mobile Application: A patient-facing app for booking appointments.
Features
C# Desktop Application
Admin View:
Manage clinic operations
Add, edit, and remove doctors and receptionists
View and generate reports
Doctor View:
Access patient records
View and manage schedules
Record patient treatments and notes
Reception View:
Handle appointments and patient check-ins
Update patient information
Schedule follow-up visits
Flutter Mobile Application
Patient Registration and Login:
Secure sign-up and sign-in
Appointment Booking:
View available slots and book appointments
Receive appointment confirmations
View Upcoming Appointments:
List of all scheduled appointments
Appointment details and reminders
Installation
C# Desktop Application
Clone the repository:

bash
Copy code
git clone https://github.com/Mariamosama300/Clinical_Project.git
Navigate to the C# project directory:

bash
Copy code
cd Clinical_Project/DesktopApp
Open the project in Visual Studio:

Open Visual Studio.
Click on File -> Open -> Project/Solution.
Select the .sln file in the DesktopApp directory.
Restore the NuGet packages:

Right-click on the solution in the Solution Explorer.
Select Restore NuGet Packages.
Build and run the application:

Press F5 to build and run the application.
Flutter Mobile Application
Ensure you have Flutter installed. For installation instructions, visit the Flutter website.

Clone the repository:

bash
Copy code
git clone https://github.com/Mariamosama300/Clinical_Project.git
Navigate to the Flutter project directory:

bash
Copy code
cd Clinical_Project/FlutterApp
Install the dependencies:

bash
Copy code
flutter pub get
Run the application:

bash
Copy code
flutter run
Usage
C# Desktop Application
Admin View:

Log in with admin credentials.
Navigate to the admin dashboard to manage clinic operations.
Use the "Add Doctor" or "Add Receptionist" buttons to add new staff.
View reports by selecting the "Reports" section.
Doctor View:

Log in with doctor credentials.
Access patient records by selecting a patient from the list.
Update treatment notes and view schedules in the "Schedule" section.
Reception View:

Log in with receptionist credentials.
Handle appointments by selecting the "Appointments" section.
Check-in patients and update their information as needed.
Flutter Mobile Application
Register and Login:

Open the app and select "Register" to create a new account.
Fill in the required details and submit.
Log in using the registered credentials.
Book Appointments:

After logging in, navigate to the "Book Appointment" section.
Select a preferred date and time from the available slots.
Confirm the appointment booking.
View Appointments:

Navigate to the "My Appointments" section to view upcoming appointments.
Select an appointment to see the details and receive reminders.
Project Structure


Flutter Mobile Application
Register and Login:

Open the app and select "Register" to create a new account.
Fill in the required details and submit.
Log in using the registered credentials.
Book Appointments:

After logging in, navigate to the "Book Appointment" section.
Select a preferred date and time from the available slots.
Confirm the appointment booking.
View Appointments:

Navigate to the "My Appointments" section to view upcoming appointments.
Select an appointment to see the details and receive reminders.
bash
Copy code
Clinical_Project/
│
├── DesktopApp/             # Directory containing the C# desktop application
│   ├── ClinicalProject.sln
│   ├── ...                 # Other C# project files and directories
│
├── FlutterApp/             # Directory containing the Flutter mobile application
│   ├── lib/
│   ├── pubspec.yaml
│   └── ...                 # Other Flutter project files and directories
│
└── README.md             


