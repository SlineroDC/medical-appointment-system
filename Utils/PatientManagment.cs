using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;
using FluentValidation;
using FluentValidationException = FluentValidation.ValidationException;

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
                            // Gather patient details
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
                            if (!int.TryParse(Console.ReadLine(), out var age))
                            {
                                Console.WriteLine("Invalid age.");
                                return;
                            }

                            var newPatient = new Patient { Name = name, DocumentId = documentId, Email = email, PhoneNumber = phone, Age = age };
                            _patientService.RegisterPatient(newPatient);

                            Console.WriteLine("\nPatient registered successfully!");
                        }
                        catch (FluentValidationException ex)
                        {
                            Console.WriteLine($"Validation errors");
                            foreach (var error in ex.Errors)
                            {
                                Console.WriteLine($"- {error.ErrorMessage}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nError registering patient: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("--- Patient List ---");
                        var patients = _patientService.GetAllPatients();
                        
                        Console.WriteLine($"DEBUG: La lista contiene {patients.Count()} paciente(s).");    

                        if (!patients.Any())
                        {
                            Console.WriteLine("No patients registered.");
                            return;
                        }
                        else
                        {
                            foreach (var patient in patients)
                            {
                                Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Document: {patient.DocumentId}, Age: {patient.Age}");
                            }
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        try
                        {
                            Console.WriteLine("--- Update Patient ---");
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


                            // We request the data to update

                            Console.Write($"Name ({patient.Name}): ");
                            var newName = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newName))
                            {
                                patient.Name = newName;
                            }

                            Console.Write($"Document ID ({patient.DocumentId}): ");
                            var newDocumentId = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newDocumentId))
                            {
                                patient.DocumentId = newDocumentId;
                            }

                            Console.Write($"Email ({patient.Email}): ");
                            var newEmail = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newEmail))
                            {
                                patient.Email = newEmail;
                            }

                            Console.Write($"Phone ({patient.PhoneNumber}): ");
                            var newPhone = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newPhone))
                            {
                                patient.PhoneNumber = newPhone;
                            }

                            Console.Write($"Age ({patient.Age}): ");
                            var ageInput = Console.ReadLine();
                            if (int.TryParse(ageInput, out var newAge))
                            {
                                patient.Age = newAge;
                            }

                            _patientService.UpdatePatient(patient);
                            Console.WriteLine("\nPatient updated successfully!");


                        }
                        catch (FluentValidationException ex)
                        {
                            Console.WriteLine($"Validation errors:");
                            foreach (var error in ex.Errors)
                            {
                                Console.WriteLine($"- {error.ErrorMessage}");
                            }
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
                            if (!int.TryParse(Console.ReadLine(), out var deleteId))
                            {
                                Console.WriteLine("Invalid ID.");
                                return;
                            }

                            var patient = _patientService.GetPatientById(deleteId);
                            if (patient == null)
                            {
                                Console.WriteLine("Patient not found.");
                                return;
                            }

                            Console.Write($"Are you sure you want to delete patient {patient.Name} (ID: {patient.Id})? (y/n): ");
                            var confirm = Console.ReadLine()?.ToLower();

                            if (confirm == "y")
                            {
                                _patientService.DeletePatient(deleteId);
                                Console.WriteLine("Patient deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Deletion cancelled.");
                            }

                        }
                        catch (FluentValidationException ex)
                        {
                            Console.WriteLine($"Validation errors:");
                            foreach (var error in ex.Errors)
                            {
                                Console.WriteLine($"- {error.ErrorMessage}");
                            }
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