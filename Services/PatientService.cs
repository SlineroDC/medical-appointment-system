using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;
using medical_appointment_system.Repositories;
using medical_appointment_system.Validators;
using FluentValidation;

namespace medical_appointment_system.Services
{
    public class PatientService(IPatientRepository patientRepository) : IPatientService
    {
        private readonly IPatientRepository _patientRepository = patientRepository;

        public IEnumerable<Patient> GetAllPatients()
        {

            return _patientRepository.GetAll();
        }

        public void RegisterPatient(Patient patient)
        {
            // Validate patient data
            var validator = new PatientValidator();
            var validationResult = validator.Validate(patient);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Check for duplicate document ID
            var existingPatient = _patientRepository.GetByDocumentId(patient.DocumentId);
            if (existingPatient != null)
            {
                throw new Exception("A patient with the same document ID already exists.");
            }
            _patientRepository.Add(patient);
        }
        public Patient? GetPatientById(int id)
        {
            return _patientRepository.GetById(id);
        }
        public void UpdatePatient(Patient patient)
        {
            var validator = new PatientValidator();
            var validationResult = validator.Validate(patient);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var existingPatient = _patientRepository.GetById(patient.Id) ?? throw new Exception("Patient not found.");
            _patientRepository.Update(patient);
        }
        public void DeletePatient(int id)
        {
            var existingPatient = _patientRepository.GetById(id);
            if (existingPatient == null)
            {
                throw new Exception("Patient not found.");
            }
            _patientRepository.Delete(id);
        }

    }
}