using Microsoft.EntityFrameworkCore;

namespace DoctorOffice.Models
{
  public class DoctorOfficeContext : DbContext
  {
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<DoctorPatient> DoctorPatient { get; set; }
    
    public DoctorOfficeContext(DbContextOptions options) : base(options) { }
  }
}