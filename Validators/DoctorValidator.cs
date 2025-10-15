using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;

namespace medical_appointment_system.Validators;

public class DoctorValidator : AbstractValidator<Doctor>
{
    public DoctorValidator()
    {
        // Rules for properties inherited from Person
        RuleFor(d => d.Name).NotEmpty().WithMessage("Doctor's name cannot be empty.");
        RuleFor(d => d.DocumentId).NotEmpty().WithMessage("Document ID cannot be empty.");
        RuleFor(d => d.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("A valid email address is required.");

        // Rule for the Doctor-specific property
        RuleFor(d => d.Specialty)
            .IsInEnum().WithMessage("A valid specialty must be selected.");
    }
}