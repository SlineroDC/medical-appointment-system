using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using medical_appointment_system.Interfaces;
using medical_appointment_system.Models;
using MimeKit;


namespace medical_appointment_system.Services
{
    public class EmailService : IEmailService
    {
        public void SendAppointmentConfirmation(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}