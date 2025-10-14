using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medical_appointment_system.Models
{
    public class Appointment
    {
    public int Id { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public AppointmentStatus Status { get; set; }

    }
}