using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Models;

namespace medical_appointment_system.Interfaces
{
    public interface IPatientService
    {
        void RegisterPatient(Patient patient);
        IEnumerable<Patient> GetAllPatients();

        Patient? GetPatientById(int id);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);
    }
}