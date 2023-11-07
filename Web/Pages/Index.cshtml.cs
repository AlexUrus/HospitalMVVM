using Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Model;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private HospitalModel _hospitalModel;
        private DoctorModelToStrMapper _doctorModelToStrMapper;
        private AppointmentTimeModelToStrMapper _appointmentTimeModelToStrMapper;

        public string NamePatient { get; set; }
        public string SurnamePatient { get; set; }
        public ICollection<string> Doctors { get; set; }
        public ICollection<string> AppointmentTimeModels { get; set; }

        public IndexModel(IRepository repository)
        {
            _hospitalModel = new HospitalModel(repository);
            _doctorModelToStrMapper = new DoctorModelToStrMapper();
            _appointmentTimeModelToStrMapper = new AppointmentTimeModelToStrMapper();
        }
        public void OnGet()
        {
            NamePatient = string.Empty;
            SurnamePatient = string.Empty;
            Doctors = DoctorsToString(_hospitalModel.DoctorModels);
            AppointmentTimeModels = AppointmentTimeModelsToString(_hospitalModel.AppointmentTimeModels);
        }

        public ICollection<string> DoctorsToString(ICollection<DoctorModel> doctorModels)
        {
            var doctorStringModels = new List<string>();

            foreach (var doctorModel in doctorModels)
            {
                doctorStringModels.Add(_doctorModelToStrMapper.ModelToString(doctorModel));
            }

            return doctorStringModels;
        }

        public ICollection<string> AppointmentTimeModelsToString(ICollection<AppointmentTimeModel> appointmentTimeModels)
        {
            var appointmentTimeStringModels = new List<string>();

            foreach (var appointmentTimeModel in appointmentTimeModels)
            {
                appointmentTimeStringModels.Add(_appointmentTimeModelToStrMapper.ModelToString(appointmentTimeModel));
            }

            return appointmentTimeStringModels;
        }
    }
}
