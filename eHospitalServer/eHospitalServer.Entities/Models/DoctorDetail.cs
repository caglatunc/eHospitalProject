using eHospitalServer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Entities.Models;
public sealed class DoctorDetail
{
    public Guid UserId { get; set; }
    public Specialty? Specialty { get; set; } 
    public List<string> WorkingDays { get; set; } = new();
}
