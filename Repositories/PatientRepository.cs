using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Data;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;

namespace medical_appointment_system.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public void Add(Patient patient)
        {
            if (Database.Patients.Any(p => p.DocumentId == patient.DocumentId))
            {
                throw new Exception("A patient with the same DocumentId already exists.");
            }
            patient.Id = Database.GetNextPatientId();
            Database.Patients.Add(patient);
        }

        public void Delete(int id)
        {
            var patientRemove = Database.Patients.FirstOrDefault(p => p.Id == id) ?? throw new Exception("Patient not found");
            Database.Patients.Remove(patientRemove);
        }

        public IEnumerable<Patient> GetAll()
        {
            if (Database.Patients.Count == 0)
            {
                throw new Exception("No patients found.");
            }
            return Database.Patients;
        }

        public Patient? GetByDocumentId(string documentId)
        {
            throw new NotImplementedException();
        }

        public Patient? GetById(int id)
        {
            var patient = Database.Patients.FirstOrDefault(p => p.Id == id) ?? throw new Exception("Patient not found");
            return patient;
        }

        public void Update(Patient patient)
        {
            var patientToUpdate = GetById(patient.Id);
            if (patientToUpdate != null)
            {
                patientToUpdate = Database.Patients.FirstOrDefault(p => p.Id == patient.Id) ?? throw new Exception("Patient not found");
                patientToUpdate.Name = patient.Name;
                patientToUpdate.DocumentId = patient.DocumentId;
                patientToUpdate.Age = patient.Age;
                patientToUpdate.PhoneNumber = patient.PhoneNumber;
                patientToUpdate.Email = patient.Email;
            }

        }
    }
}