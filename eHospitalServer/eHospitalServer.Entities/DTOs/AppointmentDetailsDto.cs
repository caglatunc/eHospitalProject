using eHospitalServer.Entities.Enums;

namespace eHospitalServer.Entities.DTOs;
public sealed record AppointmentDetailsDto(
    Guid AppointmentId,
    string DoctorName,
    Specialty Specialty,
    DateTime StartDate,
    DateTime EndDate);
