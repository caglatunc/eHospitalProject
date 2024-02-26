using eHospitalServer.Entities.Enums;

namespace eHospitalServer.Entities.Models;
public sealed class DoctorDetail
{
    public Guid UserId { get; set; }
    public Specialty Specialty { get; set; } = Specialty.GeneralMedicine;
    public List<string> WorkingDays { get; set; } = new();
}
