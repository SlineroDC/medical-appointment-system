using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;
using FluentValidation;
using medical_appointment_system.Validators;


namespace medical_appointment_system.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public AppointmentService(
       IAppointmentRepository appointmentRepository,
       IPatientRepository patientRepository,
       IDoctorRepository doctorRepository
       )
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    
    }

    public void CancelAppointment(int appointmentId)
    {
        var appointment = _appointmentRepository.GetById(appointmentId) ?? throw new Exception("Appointment not found.");
        appointment.Status = AppointmentStatus.Canceled;
        _appointmentRepository.Update(appointment);
    }

    public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date) => _appointmentRepository.GetByDate(date);
    public IEnumerable<Appointment> GetAppointmentsByDoctor(int doctorId) => _appointmentRepository.GetByDoctorId(doctorId);
    public IEnumerable<Appointment> GetAppointmentsByPatient(int patientId) => _appointmentRepository.GetByPatientId(patientId);


    public void ScheduleAppointment(int patientId, int doctorId, DateTime dateTime)
    {
        var patient = _patientRepository.GetById(patientId);
        var doctor = _doctorRepository.GetById(doctorId) ;

        // var tempAppointment = new Appointment { Patient = patient, Doctor = doctor, AppointmentDateTime = dateTime };
        // var validator = new AppointmentValidator();
        // var validationResult = validator.Validate(tempAppointment);
        // if (!validationResult.IsValid)
        // {
        //     throw new ValidationException(validationResult.Errors);
        // }

        var doctorAppointments = _appointmentRepository.GetByDoctorId(doctorId);
        if (doctorAppointments.Any(a => a.AppointmentDateTime == dateTime && a.Status == AppointmentStatus.Scheduled))
        {
            throw new Exception("The doctor already has an appointment scheduled at that time.");
        }

        var patientAppointments = _appointmentRepository.GetByPatientId(patientId);
        if (patientAppointments.Any(a => a.AppointmentDateTime == dateTime && a.Status == AppointmentStatus.Scheduled))
        {
            throw new Exception("The patient already has an appointment scheduled at that time.");
        }

        var newAppointment = new Appointment
        {
            Patient = patient,
            Doctor = doctor,
            AppointmentDateTime = dateTime,
            Status = AppointmentStatus.Scheduled
        };

        _appointmentRepository.Add(newAppointment);
    }

}