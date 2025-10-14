using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;

namespace medical_appointment_system.Utils
{
    public class PatientManagment(IPatientService patientService)
    {
        private readonly IPatientService _patientService = patientService;

        public void ShowPatientMenu()
        {
            // Simple console menu for patient management
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Patient Management ===");
                Console.WriteLine("1. Register New Patient");
                Console.WriteLine("2. List All Patients");
                Console.WriteLine("3. Update Patient");
                Console.WriteLine("4. Delete Patient");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        try
                        {

                            Console.WriteLine("\n--- Register New Patient ---");
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Identity Document: ");
                            var documentId = Console.ReadLine();
                            Console.Write("Email: ");
                            var email = Console.ReadLine();
                            Console.Write("Phone: ");
                            var phone = Console.ReadLine();
                            Console.Write("Age: ");
                            var age = int.Parse(Console.ReadLine() ?? "18");

                            var newPatient = new Patient { Name = name, DocumentId = documentId, Email = email, PhoneNumber = phone, Age = age };
                            _patientService.RegisterPatient(newPatient);

                            Console.WriteLine("\nPatient registered successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nError registering patient: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("\n--- Patient List ---");
                        var patients = _patientService.GetAllPatients();
                        if (!patients.Any())
                        {
                            Console.WriteLine("No patients registered.");
                        }
                        else
                        {
                            foreach (var Patient in patients)
                            {
                                Console.WriteLine($"ID: {Patient.Id}, Name: {Patient.Name}, Document: {Patient.Id}, Age: {Patient.Age}");
                            }
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        try
                        {
                            Console.WriteLine("\n--- Update Patient ---");
                            Console.Write("Enter Patient ID to update: ");

                            if (!int.TryParse(Console.ReadLine(), out var updateId))
                            {
                                Console.WriteLine("Invalid ID.");
                                return;
                            }

                            // Fetch existing patient
                            var patient = _patientService.GetPatientById(updateId);
                            if (patient == null)
                            {
                                Console.WriteLine("Patient not found.");
                                return;
                            }

                            Console.WriteLine($"Updating Patient: {patient.Name} (ID: {patient.Id})");
                            Console.Write($"Name ({patient.Name}): ");

                            var newName = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newName))
                            {
                                patient.Name = newName;
                            }

                            // We request the data to update

                            Console.Write($"Document ID ({patient.DocumentId}): ");
                            var newDocumentId = Console.ReadLine();

                            Console.Write($"Email ({patient.Email}): ");
                            var newEmail = Console.ReadLine();
                            


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nError updating patient: {ex.Message}");
                        }
                        break;


                    case "4":
                        try
                        {
                            Console.WriteLine("\n--- Delete Patient ---");
                            Console.Write("Enter Patient ID to delete: ");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nError deleting patient: {ex.Message}");
                        }
                        break;
                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}