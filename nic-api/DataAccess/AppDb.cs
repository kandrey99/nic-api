using Microsoft.EntityFrameworkCore;
using nic_api.Domain;

namespace nic_api.DataAccess;

#nullable disable
public sealed class AppDb : DbContextBase
{
    public AppDb(DbContextOptions<AppDb> options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Area> Areas { get; set; }
}
