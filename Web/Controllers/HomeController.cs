using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Data;
using Model.Model;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        static List<PatientWebModel> patients = new List<PatientWebModel>();
        private readonly IRepository _repository;
        private HospitalModel _hospital;

        public HomeController(IRepository repository)
        {
            _repository = repository;
            _hospital = new HospitalModel(repository);
        }

        public IActionResult Index()
        {
            return View(patients);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentWebModel appointmentWebModel)
        {
            patients.Add(appointmentWebModel.Patient);
            return RedirectToAction("Index");
        }
    }
}