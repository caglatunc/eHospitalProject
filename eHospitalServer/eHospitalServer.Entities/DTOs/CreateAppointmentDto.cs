namespace eHospitalServer.Entities.DTOs;
public sealed record CreateAppointmentDto(
    Guid DoctorId,
    Guid? PatientId,
    string FirstName,
    string LastName,
    string? Email,
    string? PhoneNumber,
    string FullAddress,
    string IdentityNumber,
    DateOnly? DateOfBirth,
    string? BloodType,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price);

