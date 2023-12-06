using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.Interfaces
{
    public interface IAppointmentRepo
    {
        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel);
        public AppointmentModel? GetAppointment(int id);
        public ICollection<AppointmentModel> GetAppointmentModelsByDoctorId(int doctorId);
    }
}
