using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Models;
namespace medical_appointment_system.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Appointment> GetByDoctorId(int doctorId);
        IEnumerable<Appointment> GetByPatientId(int patientId);
        IEnumerable<Appointment> GetByDate(DateTime date);
    }
}