using System.Collections.Generic;

namespace DoctorOffice.Models
{
  public class Doctor
  {
    public int DoctorId { get; set; }
    public string NameFirst { get; set; }
    public string NameLast { get; set; }
    public string Specialty { get; set; }
    public virtual ICollection<DoctorPatient> Patients { get; set; }

    public Doctor()
    {
      this.Patients = new HashSet<DoctorPatient>();
    }
  }
}