using Microsoft.EntityFrameworkCore;
using nic_api.Domain;

namespace nic_api.DataAccess;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Office> Offices { get; set; } = null!;
    public DbSet<Specialization> Specializations { get; set; } = null!;    
    public DbSet<Area> Areas { get; set; } = null!;
}
