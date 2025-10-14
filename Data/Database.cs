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
        public static List<Patient> Patients { get; set; } = [];
        public static List<Doctor> Doctors { get; set; } = [];
        public static List<Appointment> Appointments { get; set; } = [];
    }
}