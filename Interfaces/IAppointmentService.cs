using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Models;

namespace medical_appointment_system.Interfaces
{
    public interface IAppointmentService
    {
        void ScheduleAppointment(int patientId, int doctorId, DateTime dateTime);
        void CancelAppointment(int appointmentId);
        IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId);
        IEnumerable<Appointment> GetAppointmentsByPatient(int patientId);
        IEnumerable<Appointment> GetAppointmentsByDate(DateTime date);
    }
}