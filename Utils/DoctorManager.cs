using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;
using medical_appointment_system.Validators;
namespace medical_appointment_system.Utils

{
    public class DoctorManager(IDoctorService doctorService)
    {
        private readonly IDoctorService _doctorService = doctorService;

        public void ShowDoctorMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Doctor Management ===");
                Console.WriteLine("1. Register New Doctor");
                Console.WriteLine("2. List All Doctors");
                Console.WriteLine("3. Update Doctor");
                Console.WriteLine("4. Delete Doctor");
                Console.WriteLine("5. Filter Doctors by Specialty");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1": RegisterNewDoctor(); break;
                    case "2": ListAllDoctors(); break;
                    case "3": UpdateExistingDoctor(); break;
                    case "4": DeleteExistingDoctor(); break;
                    case "5": ListDoctorsBySpecialty(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void RegisterNewDoctor()
        {
            try
            {
                Console.WriteLine("\n--- Register New Doctor ---");
                Console.Write("Name: ");
                var name = Console.ReadLine() ?? "";
                Console.Write("Document ID: ");
                var documentId = Console.ReadLine() ?? "";
                Console.Write("Email: ");
                var email = Console.ReadLine() ?? "";
                Console.Write("Phone Number: ");
                var phone = Console.ReadLine() ?? "";

                var selectedSpecialty = SelectSpecialty();
                if (selectedSpecialty == null) return;

                var newDoctor = new Doctor { Name = name, DocumentId = documentId, Email = email, PhoneNumber = phone, Specialty = selectedSpecialty.Value };
                _doctorService.RegisterDoctor(newDoctor);

                Console.WriteLine("\nDoctor registered successfully!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("\nValidation errors:");
                foreach (var error in ex.Errors) { Console.WriteLine($"- {error.ErrorMessage}"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }

        private void ListAllDoctors()
        {
            Console.WriteLine("\n--- Doctor List ---");
            var doctors = _doctorService.GetAllDoctors();
            if (!doctors.Any())
            {
                Console.WriteLine("No doctors registered.");
            }
            else
            {
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Specialty: {doctor.Specialty}");
                }
            }
        }

        private void UpdateExistingDoctor()
        {
            try
            {
                Console.Write("\nEnter Doctor ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID format.");
                    return;
                }
                var doctor = _doctorService.GetDoctorById(id);
                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found.");
                    return;
                }

                Console.WriteLine($"\nEditing Doctor: {doctor.Name} (Press Enter to keep current value)");

                Console.Write($"New Name ({doctor.Name}): ");
                var newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName)) doctor.Name = newName;

                var newSpecialty = SelectSpecialty();
                if (newSpecialty != null) doctor.Specialty = newSpecialty.Value;

                _doctorService.UpdateDoctor(doctor);
                Console.WriteLine("\nDoctor updated successfully!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("\nValidation errors:");
                foreach (var error in ex.Errors) { Console.WriteLine($"- {error.ErrorMessage}"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }

        private void DeleteExistingDoctor()
        {
            try
            {
                Console.Write("\nEnter Doctor ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID format.");
                    return;
                }
                var doctor = _doctorService.GetDoctorById(id);
                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found.");
                    return;
                }

                Console.Write($"Are you sure you want to delete {doctor.Name}? (Y/N): ");
                if (Console.ReadLine()?.ToUpper() == "Y")
                {
                    _doctorService.DeleteDoctor(id);
                    Console.WriteLine("\nDoctor deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\nOperation canceled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }

        private void ListDoctorsBySpecialty()
        {
            var selectedSpecialty = SelectSpecialty();
            if (selectedSpecialty == null) return;

            Console.WriteLine($"\n--- Doctors with Specialty: {selectedSpecialty.Value} ---");
            var doctors = _doctorService.GetDoctorsBySpecialty(selectedSpecialty.Value.ToString());

            if (!doctors.Any())
            {
                Console.WriteLine("No doctors found for this specialty.");
            }
            else
            {
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Document: {doctor.DocumentId}");
                }
            }
        }

        private Specialty? SelectSpecialty()
        {
            Console.WriteLine("\nSelect a Specialty:");
            var specialties = Enum.GetValues(typeof(Specialty));
            for (int i = 0; i < specialties.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {specialties.GetValue(i)}");
            }
            Console.Write("Option: ");
            if (!int.TryParse(Console.ReadLine(), out int specialtyIndex) || specialtyIndex < 1 || specialtyIndex > specialties.Length)
            {
                Console.WriteLine("Invalid specialty selection. Operation canceled.");
                return null;
            }
            return (Specialty)specialties.GetValue(specialtyIndex - 1);
        }
    }
}