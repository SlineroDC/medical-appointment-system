using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Data;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;

namespace medical_appointment_system.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public void Add(Appointment entity)
        {
            if (Database.Appointments.Any(a => a.Id == entity.Id))
            {
                throw new Exception("Appointment with the same ID already exists.");
            }
            entity.Id = Database.GetNextAppointmentId();
            Database.Appointments.Add(entity);

        }

        public void Delete(int id)
        {
            var appointmentRemove = Database.Appointments.FirstOrDefault(a => a.Id == id) ?? throw new Exception("Appointment not found");
            Database.Appointments.Remove(appointmentRemove);
        }

        public IEnumerable<Appointment> GetAll()
        {
            if (Database.Appointments.Count == 0)
            {
                throw new Exception("No appointments found.");
            }
            return Database.Appointments;
        }

        public IEnumerable<Appointment> GetByDate(DateTime date)
        {
            var appointment = Database.Appointments.Where(a => a.AppointmentDateTime.Date == date.Date) ?? throw new Exception("No appointments found for the given date");
            return appointment;
        }

        public IEnumerable<Appointment> GetAppointmentByDoctorId(int doctorId)
        {

            var appointment = Database.Appointments.Where(a => a.Doctor != null && a.Doctor.Id == doctorId);
            if (!appointment.Any())
            {
                Console.WriteLine("The doctor doesnt has any appoinment");
                return [];
            }
            return appointment;
        }

        public Appointment? GetById(int id)
        {
            var appointment = Database.Appointments.FirstOrDefault(a => a.Id == id) ?? throw new Exception("Appointment not found");
            return appointment;
        }

        public IEnumerable<Appointment> GetByPatientId(int patientId)
        {
            var appointment = Database.Appointments.Where(a => a.Patient != null && a.Patient.Id == patientId) ?? throw new Exception("No appointments found for the given patient ID");
            return appointment;
            throw new NotImplementedException();
        }

        public void Update(Appointment entity)
        {
            var appointmentToUpdate = GetById(entity.Id);
            if (appointmentToUpdate != null)
            {
                appointmentToUpdate = Database.Appointments.FirstOrDefault(a => a.Id == entity.Id) ?? throw new Exception("Appointment not found");
                appointmentToUpdate.AppointmentDateTime = entity.AppointmentDateTime;
                appointmentToUpdate.Doctor = entity.Doctor;
                appointmentToUpdate.Patient = entity.Patient;
            }
        }
    }
}