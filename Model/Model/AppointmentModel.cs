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
        public int Id { get; private set; }
        public DoctorModel DoctorModel { get; private set; }
        public PatientModel PatientModel { get; private set; }
        public AppointmentTimeModel AppointmentTimeModel { get; set; }

        private DataRepository _dataRepository = DataRepository.Instance;

        public AppointmentModel(int id, DoctorModel doctorModel, PatientModel patientModel, AppointmentTimeModel appointmentTimeModel)
        {
            Id = id;
            DoctorModel = doctorModel;
            PatientModel = patientModel;
            AppointmentTimeModel = appointmentTimeModel;
        }
    }
}
