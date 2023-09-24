using Model.Data;
using System.Collections.ObjectModel;

namespace Model.Model
{
    public class HospitalModel
    {
        private static HospitalModel _instance;
        public static HospitalModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(HospitalModel))
                    {
                        if (_instance == null)
                        {
                            _instance = new HospitalModel();
                        }
                    }
                }
                return _instance;
            }
        }

        private HospitalModel()
        {
            _repository = DataRepository.Instance;

            InitTypeDoctors();
            InitDoctors();
            InitAppointmentTimes();
        }
        private DataRepository _repository;
        public ObservableCollection<DoctorModel> DoctorModels { get; private set; }
        public ObservableCollection<PatientModel> PatientModels { get; private set; }
        public ObservableCollection<AppointmentModel> AppointmentModels { get; private set; }
        public ObservableCollection<AppointmentTimeModel> AppointmentTimeModels { get; private set; }
        public ObservableCollection<TypeDoctorModel> TypeDoctorModels { get; private set; }
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

        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        { 
            _repository.CreateAppointment(patientModel, doctorModel, appointmentTimeModel);
        }

        public PatientModel GetPatientModel(string name, string surname)
        {
            return _repository.GetPatientModel(name, surname);
        }

        public DoctorModel GetDoctorModel(string name, string surname)
        {
            return _repository.GetDoctorModel(name, surname);
        }

        public AppointmentModel GetAppointmentModel(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            return _repository.GetAppointmentModel(patientModel, doctorModel, appointmentTimeModel);
        }

    }
}
