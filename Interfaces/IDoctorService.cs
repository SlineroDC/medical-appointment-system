using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Models;

namespace medical_appointment_system.Interfaces
{
    public interface IDoctorService
    {
        void RegisterDoctor(Doctor doctor);
        IEnumerable<Doctor> GetAllDoctors();
        IEnumerable<Doctor> GetDoctorsBySpecialty(string specialty);
    }
}