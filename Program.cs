using medical_appointment_system.Interfaces;
using medical_appointment_system.Repositories;
using medical_appointment_system.Services;
using medical_appointment_system.Utils;

// --- SETUP ---

// Repositories (Data Access Layer)
IPatientRepository patientRepository = new PatientRepository();
IDoctorRepository doctorRepository = new DoctorRepository();
IAppointmentRepository appointmentRepository = new AppointmentRepository();

// Services (Business Logic Layer)
IPatientService patientService = new PatientService(patientRepository);
IDoctorService doctorService = new DoctorService(doctorRepository);
IAppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository);

// UI (User Interface Layer)
var menu = new MenuManager(patientService, doctorService, appointmentService);


// --- RUN ---

// Start the application's main loop.
menu.ShowMenu();