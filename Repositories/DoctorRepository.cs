using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;
using medical_appointment_system.Data;

namespace medical_appointment_system.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        public void Add(Doctor doctor)
        {
            if (Database.Doctors.Any(d => d.Id == doctor.Id))
            {
                throw new Exception("Doctor with the same ID already exists.");
            }
            doctor.Id = Database.GetNextDoctorId();
            Database.Doctors.Add(doctor);
        }

        public void Delete(int id)
        {
            var doctorRemove = Database.Doctors.FirstOrDefault(d => d.Id == id) ?? throw new Exception("Doctor not found");
            Database.Doctors.Remove(doctorRemove);

        }

        public IEnumerable<Doctor> GetAll()
        {
            if (Database.Doctors.Count == 0)
            {
                throw new Exception("No doctors found.");
            }
            return Database.Doctors;
        }

        public Doctor? GetById(int id)
        {
            var doctorGet = Database.Doctors.FirstOrDefault(d => d.Id == id) ?? throw new Exception("Doctor not found");
            return doctorGet;
        }

        public Doctor? GetBySpecialty(Specialty specialty)
        {
            var doctorSpecialty = Database.Doctors.FirstOrDefault(d => d.Specialty == specialty);
            return doctorSpecialty;
        }

        public void Update(Doctor entity)
        {
            var doctorToUpdate = GetById(entity.Id);
            if (doctorToUpdate != null)
            {
                doctorToUpdate = Database.Doctors.FirstOrDefault(d => d.Id == entity.Id) ?? throw new Exception("Doctor not found");
                doctorToUpdate.Name = entity.Name;
                doctorToUpdate.DocumentId = entity.DocumentId;
                doctorToUpdate.PhoneNumber = entity.PhoneNumber;
                doctorToUpdate.Email = entity.Email;
                doctorToUpdate.Specialty = entity.Specialty;
            }
        }
    }
}