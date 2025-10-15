using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Doctor specific properties

namespace medical_appointment_system.Models
{
    public class Doctor : Person
    {
        public Specialty Specialty { get; set; }
    }
}