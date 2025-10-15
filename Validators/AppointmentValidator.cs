using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using medical_appointment_system.Models;

namespace medical_appointment_system.Validators
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            // The appointment must be for a valid patient and doctor.
            RuleFor(a => a.Patient).NotNull().WithMessage("A patient is required for the appointment.");
            RuleFor(a => a.Doctor).NotNull().WithMessage("A doctor is required for the appointment.");

            // The appointment date must be in the future.
            RuleFor(a => a.AppointmentDateTime)
                .GreaterThan(DateTime.Now).WithMessage("Appointment date must be in the future.");
        }

    }
}