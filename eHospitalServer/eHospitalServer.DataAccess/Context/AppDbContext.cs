using eHospitalServer.Entities.Models;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eHospitalServer.DataAccess.Context;
internal sealed class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DoctorDetail> DoctorDetails { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);

        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
    }
}
