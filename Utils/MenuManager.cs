using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;
using medical_appointment_system.Utils;
namespace medical_appointment_system.Utils;

public class MenuManager(IPatientService patientService, IDoctorService doctorService, IAppointmentService appointmentService)
{
    private readonly IPatientService _patientService = patientService;
    private readonly IDoctorService _doctorService = doctorService;
    private readonly IAppointmentService _appointmentService = appointmentService;

    // The main loop of the application now lives here
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Medical Appointment Management System ===");
            Console.WriteLine("1. Patient Management");
            Console.WriteLine("2. Doctor Management");
            Console.WriteLine("3. Appointment Management");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    var patientMenu = new PatientManagment(_patientService);
                    patientMenu.ShowPatientMenu();
                    break;
                case "2":
                    // ShowDoctorMenu(); (We will create it later)
                    break;
                case "3":
                    // ShowAppointmentMenu(); (We will create it later)
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }


}
