using Model.Data;
using System.Collections.ObjectModel;

namespace Model.Model
{
    public class HospitalModel
    {
        public HospitalModel()
        {
            _repository = new DataRepository();

            InitTypeDoctors();
            InitDoctors();
            InitAppointmentTimes();
        }

        private DataRepository _repository;
        public ICollection<DoctorModel> DoctorModels { get; private set; }
        public ICollection<PatientModel> PatientModels { get; private set; }
        public ICollection<AppointmentModel> AppointmentModels { get; private set; }
        public ICollection<AppointmentTimeModel> AppointmentTimeModels { get; private set; }
        public ICollection<TypeDoctorModel> TypeDoctorModels { get; private set; }
        private void InitTypeDoctors()
        {
            TypeDoctorModels = _repository.GetTypeDoctorModels();

            if (TypeDoctorModels.Count == 0)
            {
                _repository.CreateTypeDoctorModels();
                TypeDoctorModels = _repository.GetTypeDoctorModels();
            }
        }

        private void InitDoctors()
        {
            DoctorModels = _repository.GetDoctorModels();

            if (DoctorModels.Count == 0)
            {
                _repository.CreateDoctors();
                DoctorModels = _repository.GetDoctorModels();
            }
        }

        public void InitAppointmentTimes()
        {
            AppointmentTimeModels = _repository.GetAppointmentTimes();

            if (AppointmentTimeModels.Count == 0)
            {
                _repository.CreateAppointmentTimes();
                AppointmentTimeModels = _repository.GetAppointmentTimes();
            }
        }

        public void CreatePatient(string name, string surname)
        {
            _repository.CreatePatient(name, surname);
        }

        public bool PatientExists(string name, string surname)
        {
            return _repository.PatientExists(name, surname);
        }

        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        { 
            _repository.CreateAppointment(patientModel, doctorModel, appointmentTimeModel);
        }

        public PatientModel GetPatientModel(string name, string surname)
        {
            return _repository.GetPatientModel(name, surname);
        }

        public ICollection<AppointmentTimeModel> GetListFreeTimesDoctor(DoctorModel doctorModel)
        {
            return _repository.GetListFreeTimesDoctor(doctorModel);
        }
    }
}
