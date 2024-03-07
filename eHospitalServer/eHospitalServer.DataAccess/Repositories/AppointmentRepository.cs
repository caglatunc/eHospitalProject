using eHospitalServer.DataAccess.Context;
using eHospitalServer.Entities.Models;
using eHospitalServer.Entities.Repositories;
using GenericRepository;

namespace eHospitalServer.DataAccess.Repositories;
internal sealed class AppointmentRepository : Repository<Appointment, AppDbContext>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context)
    {
    }
}
