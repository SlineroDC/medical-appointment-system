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
        public static List<Patient> Patients { get; set; } = [];
        public static List<Doctor> Doctors { get; set; } = [];
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