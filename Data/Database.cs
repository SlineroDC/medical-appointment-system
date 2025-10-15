using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Models;

namespace medical_appointment_system.Data
{
    public class Database
    {
        // In-memory storage with lists
        public static List<Patient> Patients { get; set; } = [
       new Patient
            {
                Id = 1,
                Name = "Juan Perez",
                DocumentId = "123456789",
                Email = "juan.perez@example.com",
                PhoneNumber = "555-1234",
                Age = 30
            },
            new Patient
            {
                Id = 2,
                Name = "Maria Lopez",
                DocumentId = "987654321",
                Email = "maria.lopez@example.com",
                PhoneNumber = "555-5678",
                Age = 25
            },
            new Patient
            {
                Id = 3,
                Name = "Carlos Sanchez",
                DocumentId = "456789123",
                Email = "carlos.sanchez@example.com",
                PhoneNumber = "555-8765",
                Age = 40
            }
        ];
        public static List<Doctor> Doctors { get; set; } = [
            new Doctor
            {
                Id = 1,
                Name = "Dr. Ana Torres",
                DocumentId = "1122334455",
                Email = "ana.torres@example.com",
                PhoneNumber = "555-1111",
                Specialty = Specialty.Cardiology
            },
            new Doctor
            {
                Id = 2,
                Name = "Dr. Luis Martinez",
                DocumentId = "2233445566",
                Email = "luis.martinez@example.com",
                PhoneNumber = "555-2222",
                Specialty = Specialty.Dermatology
            },
            new Doctor
            {
                Id = 3,
                Name = "Dr. Sofia Ramirez",
                DocumentId = "3344556677",
                Email = "sofia.ramirez@example.com",
                PhoneNumber = "555-3333",
                Specialty = Specialty.Pediatrics
            }
        ];
        public static List<Appointment> Appointments { get; set; } = [];

        //Create method for generating IDs

        public static int GetNextPatientId()
        {
            return Patients.Count != 0 ? Patients.Max(p => p.Id) + 1 : 1;
        }

        public static int GetNextDoctorId()
        {
            return Doctors.Count != 0 ? Doctors.Max(d => d.Id) + 1 : 1;
        }

        public static int GetNextAppointmentId()
        {
            return Appointments.Count != 0 ? Appointments.Max(a => a.Id) + 1 : 1;
        }
    }
}