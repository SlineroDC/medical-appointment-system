using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Models;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Validators;
using FluentValidation;
using medical_appointment_system.Data;



namespace medical_appointment_system.Services
{
    public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository = doctorRepository;

        // Validates and registers a new doctor in the system.
        public void RegisterDoctor(Doctor doctor)
        {
            // First, validate the doctor's data format.
            var validator = new DoctorValidator();
            var validationResult = validator.Validate(doctor);

            // If invalid, throw an exception with the errors.
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // If validation passes, call the repository to add the doctor.
            _doctorRepository.Add(doctor);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            // Returns the full list of doctors from the repository.
            return _doctorRepository.GetAll();
        }

        public IEnumerable<Doctor> GetDoctorsBySpecialty(Specialty specialty)
        {
            // Returns a list of doctors filtered by their specialty.
            return _doctorRepository.GetBySpecialty(specialty);
        }

        public Doctor? GetDoctorById(int id)
        {
            // Fetches a single doctor by their unique ID.
            return _doctorRepository.GetById(id);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            // First, validate the updated data to ensure it is correct.
            var validator = new DoctorValidator();
            var validationResult = validator.Validate(doctor);

            // If the new data is invalid, stop and throw an error.
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // If the data is valid, call the repository to save the changes.
            _doctorRepository.Update(doctor);
        }

        public void DeleteDoctor(int id)
        {
            // Calls the repository to remove a doctor by their ID.
            _doctorRepository.Delete(id);
        }

        public IEnumerable<Doctor> GetDoctorsBySpecialty(string
         specialty)
        {
            if (!Enum.TryParse<Specialty>(specialty, true, out Specialty parsedSpecialty))
            {
                return [];
            }

            return Database.Doctors.Where(d => d.Specialty == parsedSpecialty);

        }
    }
}