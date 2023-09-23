using Model.Data;
using Model.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class MainModel
    {
        private AppointmentModel _appointmentModel;
        private AppointmentTimeModel _appointmentTimeModel;
        private DoctorModel _doctorModel;
        private PatientModel _patientModel;
        private DataRepository _dataRepository = DataRepository.Instance;

    }
}
