using Model.Data;
using Model.EFModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class AppointmentModel
    {
        public int Id { get; }
        public DoctorModel DoctorModel { get; set; }
        public PatientModel PatientModel { get; set; }
        public AppointmentTimeModel AppointmentTimeModel { get; set; }

        private DataRepository _dataRepository = DataRepository.Instance;

        private void SaveAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            _dataRepository.SetAppointment(new AppointmentModel()
            {
                DoctorModel = doctorModel,
                PatientModel = patientModel,
                AppointmentTimeModel = appointmentTimeModel
            });
        }
    }

}
