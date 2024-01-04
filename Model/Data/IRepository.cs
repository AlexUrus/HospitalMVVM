using Model.EFModel;
using Model.Model;
using Model.TableFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public interface IRepository
    {
        public ICollection<TypeDoctorModel> GetTypeDoctorModels();
        public ICollection<DoctorModel> GetDoctorModels();
        public ICollection<AppointmentTimeModel> GetAppointmentTimes();
        public void InitTypeDoctorModels();
        public void InitDoctors(ICollection<TypeDoctorModel> typeDoctorModels);
        public void InitAppointmentTimes();
        public void CreatePatient(string name, string surname);
        public bool PatientExists(string name, string surname);
        public PatientModel GetPatientModel(string name, string surname);
        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel);
        public AppointmentModel? GetAppointmentModel(int id);
        public ICollection<AppointmentModel> GetAppointmentModelsByDoctorId(int doctorId);
        public List<DoctorShedule> GetSheduleDoctor();
    }
}
