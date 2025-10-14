using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;

namespace medical_appointment_system.Repositories
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    public class AppointmentRepository : IAppointmentRepository
    {
        public void Add(Appointment entity)
        {

            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetByDoctorId(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Appointment? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }

        public void Update(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}