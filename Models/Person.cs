namespace medical_appointment_system.Models;

//Common properties for both Doctor and Patient
public abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? DocumentId { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}