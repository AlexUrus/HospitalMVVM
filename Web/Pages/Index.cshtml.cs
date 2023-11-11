using Azure;
using Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Data;
using Model.Model;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private HospitalModel _hospitalModel;
        private DoctorModelToStrMapper _doctorModelToStrMapper;
        private AppointmentTimeModelToStrMapper _appointmentTimeModelToStrMapper;

        [BindProperty]
        public string NamePatient { get; set; } = "";
        [BindProperty]
        public string SurnamePatient { get; set; } = "";
        public ICollection<SelectListItem> Doctors { get; set; }
        public ICollection<SelectListItem> AppointmentTimes { get; set; }

        public string NameBtnCreateAppointment { get; set; } = "Записаться на прием";
        public string Message { get; private set; } = "";

        public IndexModel(IRepository repository)
        {
            _hospitalModel = new HospitalModel(repository);
            _doctorModelToStrMapper = new DoctorModelToStrMapper();
            _appointmentTimeModelToStrMapper = new AppointmentTimeModelToStrMapper();
        }
        public void OnGet()
        {
            Message = "Введите данные";
            Doctors = DoctorsToString(_hospitalModel.DoctorModels);
            AppointmentTimes = AppointmentTimesToString(_hospitalModel.AppointmentTimeModels);
        }

        public void OnPost(string namePatient, string surnamePatient)
        {
            NamePatient = namePatient; SurnamePatient = surnamePatient;
            Message = $"Имя: {NamePatient} Фамилия: {SurnamePatient}";
        }

        public ICollection<SelectListItem> DoctorsToString(ICollection<DoctorModel> doctorModels)
        {
            var doctorStringModels = new List<SelectListItem>();

            foreach (var doctorModel in doctorModels)
            {
                string modelToString = _doctorModelToStrMapper.ModelToString(doctorModel);
                doctorStringModels.Add(new SelectListItem
                {   
                    Value = modelToString,
                    Text = modelToString
                });
            }

            return doctorStringModels;
        }

        public ICollection<SelectListItem> AppointmentTimesToString(ICollection<AppointmentTimeModel> appointmentTimeModels)
        {
            var appointmentTimeStringModels = new List<SelectListItem>();

            foreach (var appointmentTimeModel in appointmentTimeModels)
            {
                string modelToString = _appointmentTimeModelToStrMapper.ModelToString(appointmentTimeModel);
                appointmentTimeStringModels.Add(new SelectListItem
                {
                    Value = modelToString,
                    Text = modelToString
                });
            }

            return appointmentTimeStringModels;
        }

        public void CallCreateAppointment()
        {

        }
    }
}
