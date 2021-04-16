using DoctorOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DoctorOffice.Controllers
{
  public class PatientsController : Controller
  {
    private readonly DoctorOfficeContext _db;

    public PatientsController(DoctorOfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patients.ToList());
    }

    public ActionResult Create()
    {
      IEnumerable<SelectListItem> IdList = _db.Doctors.Select(s => new SelectListItem
      {
        Value = s.DoctorId.ToString(),
        Text = $"{s.NameFirst} {s.NameLast} ({s.Specialty})"
      });
      ViewBag.DoctorId = IdList;
      ViewBag.ListCount = IdList.Count<SelectListItem>();
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patient patient, int DoctorId)
    {
      _db.Patients.Add(patient);
      if(DoctorId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = DoctorId, PatientId = patient.PatientId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Patient thisPatient = _db.Patients
        .Include(patient => patient.Doctors)
          .ThenInclude(join => join.Doctor)
        .FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }

    public ActionResult Edit(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      IEnumerable<SelectListItem> IdList = _db.Doctors.Select(s => new SelectListItem
      {
        Value = s.DoctorId.ToString(),
        Text = $"{s.NameFirst} {s.NameLast} ({s.Specialty})"
      });
      ViewBag.DoctorId = IdList;
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult Edit(Patient patient, int DoctorId)
    {
      bool unique = true;
      List<DoctorPatient> doctorList = _db.Patients
        .Where(i => i.PatientId == patient.PatientId)
        .Select(i => i.Doctors).ToList()[0].ToList();
      foreach(DoctorPatient doctorPatient in doctorList)
      {
        if(doctorPatient.DoctorId == DoctorId)
        {
          unique = false;
        }
      }
      if(DoctorId != 0 && unique)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = DoctorId, PatientId = patient.PatientId });
      }
      _db.Entry(patient).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddDoctor(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      IEnumerable<SelectListItem> IdList = _db.Doctors.Select(s => new SelectListItem
      {
        Value = s.DoctorId.ToString(),
        Text = $"{s.NameFirst} {s.NameLast} ({s.Specialty})"
      });
      ViewBag.DoctorId = IdList;
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult AddDoctor(Patient patient, int DoctorId)
    {
      bool unique = true;
      List<DoctorPatient> doctorList = _db.Patients
        .Where(i => i.PatientId == patient.PatientId)
        .Select(i => i.Doctors).ToList()[0].ToList();
      foreach (DoctorPatient doctorPatient in doctorList)
      {
        if (doctorPatient.DoctorId == DoctorId)
        {
          unique = false;
        }
      }
      if(DoctorId != 0 && unique)
      {
        _db.DoctorPatient.Add(new DoctorPatient() {DoctorId = DoctorId, PatientId = patient.PatientId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      _db.Patients.Remove(thisPatient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteDoctor(int joinId)
    {
      DoctorPatient joinEntry = _db.DoctorPatient.FirstOrDefault(entry => entry.DoctorPatientId == joinId);
      _db.DoctorPatient.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}