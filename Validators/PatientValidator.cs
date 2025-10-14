using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using medical_appointment_system.Models;

namespace medical_appointment_system.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(p => p.DocumentId).NotEmpty().WithMessage("Document ID cannot be empty.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("The email format is not valid.");

            RuleFor(p => p.Age)
                .InclusiveBetween(1, 120).WithMessage("Age must be a realistic value between 1 and 120.");
        }

    }
}