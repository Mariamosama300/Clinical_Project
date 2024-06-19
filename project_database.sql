CREATE DATABASE PROJECT1;
use PROJECT1;
-- Supertype table
CREATE TABLE Employee (
     Employee_Id INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(255) ,
    education VARCHAR(255),
    password VARCHAR(255) ,
    phone_number VARCHAR(20) ,
    Gender VARCHAR(10),
    National_ID VARCHAR(20) ,
    birthday DATE,
    Name VARCHAR(255) -- Composite field combining first name and last name
);

INSERT INTO Employee(email,password) VALUES ('ak@doc.com','123');

SELECT * FROM Employee;

-- Subtype table for Managers (inherits from Employee)
CREATE TABLE Manager (
    Employee_Id INT,
    Job_description VARCHAR(255),
    FOREIGN KEY (Employee_Id) REFERENCES Employee(Employee_Id) ON DELETE CASCADE
);


-- Subtype table for Doctors (inherits from Employee)
CREATE TABLE Doctor (
    Employee_Id INT, -- Use the same primary key as in Employee
    major VARCHAR(100),
    experience VARCHAR(100),
    FOREIGN KEY (Employee_Id) REFERENCES Employee(Employee_Id) ON DELETE CASCADE
);

-- Subtype table for Receptionists (inherits from Employee)
CREATE TABLE Receptionist (
    Employee_Id INT , -- Use the same primary key as in Employee
    salary DECIMAL(10, 2), -- Assuming salary is a decimal value
   --  experience VARCHAR(100),
    FOREIGN KEY (Employee_Id) REFERENCES Employee(Employee_Id) ON DELETE CASCADE
);

-- Insert data into the Employee table
INSERT INTO Employee ( email, education, password, phone_number, Gender, National_ID, birthday, Name)
VALUES ( 'manager@example.com', 'MBA', 'password123', '123-456-7890', 'Male', '123ABC', '1990-01-01', 'John Doe');

-- Insert data into the Manager subtype table
INSERT INTO Manager ( Job_description)
VALUES ( 'Manager of Operations');

-- ////////////////////////////////////////////////////////////////////////////////////////////////

create table Tools_Materials(
material_id INT PRIMARY KEY AUTO_INCREMENT,
material_name VARCHAR(255),
item_number INT,
minimum_number INT,
package_number INT
);

-- ////////////////////////////////////////////////////////////////////////////////////////////////

-- contact information who send and receive the messages
create table Users(
user_id INT PRIMARY KEY AUTO_INCREMENT,
user_name VARCHAR(50) NOT NULL UNIQUE
);

-- messages table
create table Messages(
message_id INT PRIMARY KEY AUTO_INCREMENT,
sender_id INT,
receiver_id INT,
message_text TEXT,
sent_datetime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
FOREIGN KEY (sender_id) REFERENCES Users(user_id),
FOREIGN KEY (receiver_id) REFERENCES Users(user_id)
);

CREATE TABLE ocr_results (
    id INT PRIMARY KEY,
    extracted_text TEXT
);

CREATE TABLE dicom_images (
    id INT  auto_increment primary KEY,
    patient_id INT,
    image_data longblob,
	FOREIGN KEY (patient_id) REFERENCES MEDICAL_FORM(form_id)
);
-- drop table dicom_images;

CREATE TABLE pie_chart_data (
    data_id INT AUTO_INCREMENT PRIMARY KEY,
    category VARCHAR(255) NOT NULL,
    value DECIMAL(10, 2) NOT NULL,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE chart_data (
    data_id INT AUTO_INCREMENT PRIMARY KEY,
    x_value DECIMAL(10, 2) NOT NULL,
    y_value DECIMAL(10, 2) NOT NULL,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table for feedback form
CREATE TABLE patient_feedback (
    feedback_id INT AUTO_INCREMENT PRIMARY KEY,
    gender ENUM('Male', 'Female') NOT NULL,
    birthday DATE NOT NULL,
    appointement ENUM('Very satisfied','satisfied', 'Unsatisfied') NOT NULL,
    reception ENUM('Very satisfied','satisfied', 'Unsatisfied') NOT NULL,
    waittime ENUM('Very satisfied','satisfied', 'Unsatisfied') NOT NULL,
    nurse ENUM('Very satisfied','satisfied', 'Unsatisfied') NOT NULL,
    doctor ENUM('Very satisfied','satisfied', 'Unsatisfied') NOT NULL,
    improve_services TEXT,
    feedback_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table for payment procedures
CREATE TABLE procedure_payments (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    procedure_name VARCHAR(255) NOT NULL,
    procedure_date DATE NOT NULL,
    amount DECIMAL(10, 2) NOT NULL,
    payment_date DATE NOT NULL,
    payment_method VARCHAR(50) NOT NULL,  deposite int not null , unbilled int, 
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table for appointments
CREATE TABLE appointments (
    appointment_id INT AUTO_INCREMENT PRIMARY KEY,
    doctor_id INT,
    patient_id VARCHAR(255) NOT NULL,
    appointment_date DATETIME NOT NULL,
    -- appointment_time TIME NOT NULL,
    -- dentist_name VARCHAR(255) NOT NULL,
--     reason_for_visit TEXT,
--     status ENUM('Scheduled', 'Canceled', 'Completed') DEFAULT 'Scheduled',
--     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
	FOREIGN KEY (doctor_id) REFERENCES Doctor(Employee_Id)
);

-- INSERT INTO appointments VALUES ('1', '4', ) ;

-- Table for doctor schedule
CREATE TABLE DoctorSchedule (
    ScheduleID INT PRIMARY KEY,
    appointment_id INT,
    Status VARCHAR(50),
    DateTime DATETIME,
    FOREIGN KEY (appointment_id) REFERENCES appointments (appointment_id)
);


CREATE TABLE dentalchartinfo (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Symptoms TEXT,
    Diagnosis varchar(255),
    TreatmentPlan TEXT,
    Notes TEXT
);

create table service(
service_id int primary key,
doctor_id INT,  -- fk from primary of table called sign up  
cost int ,
speciality varchar(20),
FOREIGN KEY (doctor_id) REFERENCES Doctor(Employee_Id)
);

-- Table for medical history form connected to a specific doctor with a foreign key
create table MEDICAL_FORM(
form_id INT PRIMARY KEY AUTO_INCREMENT,
Mname VARCHAR(255), -- Composite field combining first name and last name
appointment_id INT,
job_id INT,
address VARCHAR(255),
email VARCHAR(255),
phone VARCHAR(20),
gender BOOL,
Bdate DATE,
job VARCHAR(255),
Hphone VARCHAR(20),
last_treatment VARCHAR(20),
contact_Way SET('Phone', 'SMS', 'Whatsapp') NOT NULL,
recommended ENUM('Facebook','Friend','Website','Insurance Company','Sign','Other social media','Doctor') NOT NULL,
-- attending_treatment ENUM('Yes','No') NOT NULL,
-- taking_medicines ENUM('Yes','No') NOT NULL,
-- allergic ENUM('Yes','No') NOT NULL,
-- fever ENUM('Yes','No') NOT NULL,
-- disease ENUM('Yes','No') NOT NULL,
-- heart_problems ENUM('Yes','No') NOT NULL,
-- bad_reaction ENUM('Yes','No') NOT NULL,
-- heart_surgery ENUM('Yes','No') NOT NULL,
-- chest_condition ENUM('Yes','No') NOT NULL,
-- blackouts ENUM('Yes','No') NOT NULL,
-- diabetes ENUM('Yes','No') NOT NULL,
-- bruise ENUM('Yes','No') NOT NULL,
-- smoke ENUM('Yes','No') NOT NULL,
FOREIGN KEY (job_id) REFERENCES Doctor(Employee_Id)
-- FOREIGN KEY (appointment_id) REFERENCES appointments(appointment_id)
);

-- Table to store prescriptions
CREATE TABLE Prescriptions (
     patient_id INT,
	 prescription_id INT AUTO_INCREMENT PRIMARY KEY,
     note TEXT,
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
     FOREIGN KEY (patient_id) REFERENCES MEDICAL_FORM(form_id)
);

-- Table to store drugs within a prescription
CREATE TABLE Drugs (
    drug_id INT AUTO_INCREMENT PRIMARY KEY,
    prescription_id INT,
     drug_name VARCHAR(600),
     dosage VARCHAR(25) ,
     time_hours VARCHAR(100),
     days VARCHAR(20),
     until_date DATE,
    FOREIGN KEY (prescription_id) REFERENCES prescriptions(prescription_id)  ON DELETE CASCADE
);
select *
from drugs;

INSERT INTO drugs (drug_name, dosage) VALUES ('aspirin', '2'),
('panadol', '5');


select *
from prescriptions;

select *
from appointments;
select *
from Employee;

ALTER USER 'root'@'localhost'identified WITH mysql_native_password BY '14107';