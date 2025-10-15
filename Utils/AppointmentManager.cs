using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using FluentValidationException = FluentValidation.ValidationException;
using System.Globalization;
using Org.BouncyCastle.Math.Field;

namespace medical_appointment_system.Utils;

public class AppointmentManager(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService)
{
    private readonly IAppointmentService _appointmentService = appointmentService;
    private readonly IPatientService _patientService = patientService;
    private readonly IDoctorService _doctorService = doctorService;
    public void ShowAppoinmentMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Appointment Management ===");
            Console.WriteLine("1. Schedule New Appointment");
            Console.WriteLine("2. Cancel Appointment");
            Console.WriteLine("3. List Appointments by Doctor");
            Console.WriteLine("4. List Appointments by Patient");
            Console.WriteLine("5. List Appointments by Date");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1": ScheduleNewAppointment(); break;
                case "2": CancelExistingAppointment(); break;
                case "3": ListAppointmentsByDoctor(); break;
                case "4": ListAppointmentsByPatient(); break;
                case "5": ListAppointmentsByDate(); break;
                case "6": return;
                default: Console.WriteLine("Invalid option."); break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void ScheduleNewAppointment()
    {
        try
        {
            Console.WriteLine("\n--- Schedule New Appointment ---");

            Console.WriteLine("\nAvailable Patients:");
            var patients = _patientService.GetAllPatients();
            if (!patients.Any()) { Console.WriteLine("No patients available to schedule."); return; }
            foreach (var p in patients) { Console.WriteLine($"  ID: {p.Id}, Name: {p.Name}"); }
            Console.Write("Enter Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId)) { Console.WriteLine("Invalid ID format."); return; }
            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine($"No patient found with ID {patientId}. Operation canceled.");
                return;
            }

            Console.WriteLine("\nAvailable Doctors:");
            var doctors = _doctorService.GetAllDoctors();
            if (!doctors.Any()) { Console.WriteLine("No doctors available to schedule."); return; }
            foreach (var d in doctors) { Console.WriteLine($"  ID: {d.Id}, Name: {d.Name}, Specialty: {d.Specialty}"); }
            Console.Write("Enter Doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId)) { Console.WriteLine("Invalid ID format."); return; }

            var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);
            if (doctor == null)
            {
                Console.WriteLine($"No patient found with ID {doctorId}. Operation canceled.");
                return;
            }


            Console.Write("Enter Date and Time (e.g., 2025-12-31 14:30): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime appointmentDate))
            {
                Console.WriteLine("Invalid date format. Use yyyy-MM-dd HH:mm.");
                return;
            }

            _appointmentService.ScheduleAppointment(patientId, doctorId, appointmentDate);
            Console.WriteLine("\nAppointment scheduled successfully!");
        }
        catch (FluentValidationException ex)
        {
            Console.WriteLine("Validation errors:");
            foreach (var error in ex.Errors) { Console.WriteLine($"- {error.ErrorMessage}"); }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError scheduling appointment: {ex.Message}");
        }
    }

    private void CancelExistingAppointment()
    {
        try
        {
            Console.WriteLine("\n--- Cancel Appointment ---");
            Console.Write("Enter the ID of the appointment to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID format."); return; }

            _appointmentService.CancelAppointment(id);
            Console.WriteLine("\nAppointment canceled successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError canceling appointment: {ex.Message}");
        }
    }

    private void ListAppointmentsByDoctor()
    {
        try
        {
            Console.WriteLine("\n--- List Appointments by Doctor ---");
            Console.Write("Enter Doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID format."); return; }

            var appointments = _appointmentService.GetAppointmentsByDoctor(id);
            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for this doctor.");
            }
            else
            {
                Console.WriteLine($"\nAppointments for Doctor ID: {id}");
                foreach (var app in appointments)
                {
                    Console.WriteLine($"  - ID: {app.Id}, Date: {app.AppointmentDateTime}, Patient: {app.Patient?.Name}, Status: {app.Status}");
                }
            }
        }
        catch (FluentValidationException ex)
        {
            Console.WriteLine("Validation errors:");
            foreach (var error in ex.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}");
        }
    }

    private void ListAppointmentsByPatient()
    {
        try
        {
            Console.WriteLine("\n--- List Appointments by Patient ---");
            Console.Write("Enter Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID format."); return; }

            var appointments = _appointmentService.GetAppointmentsByPatient(id);
            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for this patient.");
            }
            else
            {
                Console.WriteLine($"\nAppointments for Patient ID: {id}");
                foreach (var app in appointments)
                {
                    Console.WriteLine($"  - ID: {app.Id}, Date: {app.AppointmentDateTime}, Doctor: {app.Doctor?.Name}, Status: {app.Status}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}");
        }
    }

    private void ListAppointmentsByDate()
    {
        try
        {
            Console.WriteLine("\n--- List Appointments by Date ---");
            Console.Write("Enter Date (yyyy-MM-dd): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date format. Use yyyy-MM-dd.");
                return;
            }

            var appointments = _appointmentService.GetAppointmentsByDate(date);
            if (!appointments.Any())
            {
                Console.WriteLine($"No appointments found for {date:d}.");
            }
            else
            {
                Console.WriteLine($"\nAppointments for {date:d}:");
                foreach (var app in appointments)
                {
                    Console.WriteLine($"  - ID: {app.Id}, Time: {app.AppointmentDateTime.ToShortTimeString()}, Doctor: {app.Doctor?.Name}, Patient: {app.Patient?.Name}, Status: {app.Status}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}");
        }

    }
}

