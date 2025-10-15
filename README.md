# Medical Appointment Management System

A C# console application designed to digitize and manage medical appointments, patients, and doctors for the San Vicente Hospital, replacing their manual, paper-based system.  This project is developed as a performance test evaluation.

---
| Coder Info    |                |
|---------------|----------------|
| **Name** | [Sebastian Linero De Castro]    |
| **Clan** | [Caiman]    |
| **Email** | [sebastianlinero15@gmail.com]   |
| **Document** | [1193266466]      |


---

## 1. Overview
This system addresses the challenges of a manual appointment management process, such as data duplication, difficulty in retrieving information, and potential data loss.It provides a centralized, robust, and efficient solution for managing hospital records, ensuring data integrity and accessibility. 

## 2. Features
The application implements the following core functionalities:

* **Patient Management**:
    * Register, update, and delete patients.
    * List all registered patients.
    * Prevents duplicate patients based on their unique document ID.

* **Doctor Management**:
    * Register, update, and delete doctors.
    * List all registered doctors.
    * Filter the doctor list by specialty. 

* **Appointment Management**:
    * Schedule new medical appointments, assigning a patient and a doctor. 
    * Cancel existing appointments.
    * Automatic validation to prevent scheduling conflicts for both patients and doctors at the same time. 
    * List appointments by patient, doctor, or date.

* **Data Validation**:
    * Robust data validation using **FluentValidation** for entities like Patients (e.g., valid email format, realistic age).
    * Clear and user-friendly error messages for invalid data input. 
## 3. Tech Stack & Architectural Principles

* **Backend**: C# (.NET 8)
* **Application Type**: Console Application
* **Architecture**:
    * **Repository Pattern**: Decouples the business logic from the data access layer.
    * **Service Layer**: Contains all business logic and rules.
    * **Separation of Concerns (SoC)**: Code is organized into distinct layers (Models, Interfaces, Repositories, Services, Validators, Utils).
* **Data Store**: In-Memory data using static lists.
* **Validation**: FluentValidation library for robust and readable validation rules.

## 4. Prerequisites
To build and run this project, you will need:
* .NET 8 SDK or later. [cite: 80] You can download it [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

## 5. Getting Started: Installation & Execution
Follow these steps to get the application running on your local machine. [cite: 91]

1.  **Clone the repository:**
    Open your terminal and run the following command:
    ```bash
    git clone [https://github.com/SlineroDC/medical-appointment-system.git]
    ```

2.  **Navigate to the project directory:**
    ```bash
    cd [medical-appointment-system]
    ```

3.  **Build the project:**
    This command will restore the necessary dependencies (like FluentValidation) and compile the code.
    ```bash
    dotnet build
    ```

4.  **Run the application:**
    ```bash
    dotnet run
    ```
The main menu of the medical appointment system will now be displayed in your console.

## 6. Usage
Once the application is running, you will see the main menu. Use the number keys `1`, `2`, `3`, etc., followed by `Enter` to navigate through the different management modules (Patients, Doctors, Appointments) and their respective sub-menus. Follow the on-screen prompts to perform actions like registering, listing, or deleting records.

## 7. Screenshots
[cite_start]Here are some examples of the application in action. [cite: 84]

### Main Menu
![Main Menu](path/to/your/screenshot_main_menu.png)

### Patient Registration & Validation
![Patient Registration](path/to/your/screenshot_patient_registration.png)

### Appointment Scheduling
![Appointment Scheduling](path/to/your/screenshot_appointment_scheduling.png)