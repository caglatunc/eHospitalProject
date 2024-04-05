using eHospitalServer.Entities.Models;
using GenericRepository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace eHospitalServer.Entities.Repositories;
public interface IAppointmentRepository : IRepository<Appointment>
{
}
