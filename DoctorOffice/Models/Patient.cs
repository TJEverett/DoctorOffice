using System;
using System.Collections.Generic;

namespace DoctorOffice.Models
{
  public class Patient
  {
    public int PatientId { get; set; }
    public string NameFirst { get; set; }
    public string NameLast { get; set; }
    public DateTime Birthday { get; set; }
    public virtual ICollection<DoctorPatient> Doctors { get; }

    public Patient()
    {
      this.Doctors = new HashSet<DoctorPatient>();
    }
  }
}