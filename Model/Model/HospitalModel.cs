using Data;
using Model.EFModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class HospitalModel
    {
        public static HospitalModel Instance {
            get
            {
                if (Instance == null)
                    Instance = new HospitalModel();
                return Instance;
            } 
            private set
            {
                Instance = value;
            }
        }
        private DataRepository _repository;
        public ObservableCollection<DoctorModel> DoctorModels { get; private set; }
        public ObservableCollection<PatientModel> PatientModels { get; private set; }
        public ObservableCollection<AppointmentModel> AppointmentModels { get; private set; }
        public ObservableCollection<AppointmentTimeModel> AppointmentTimeModels { get; private set; }

        private HospitalModel() 
        {
            _repository = DataRepository.Instance;
            InitializateDoctors();
            InitializateAppointmentTimes();
        }

        private void InitializateDoctors()
        {
            DoctorModels = _repository.GetDoctors();

            if (DoctorModels.Count == 0)
            {
                _repository.CreateDoctors();
                DoctorModels = _repository.GetDoctors();
            }
        }

        public void InitializateAppointmentTimes()
        {
            DataRepository dataRepository = DataRepository.Instance;
            AppointmentTimeModels = dataRepository.GetAppointmentTimes();

            if (AppointmentTimeModels.Count == 0)
            {
                dataRepository.CreateAppointmentTimes();
            }
        }

        public void CreatePatient(string name, string surname)
        {
            _repository.CreatePatient(name,surname);
        }
        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            _repository.SetAppointment();

            ClearInputFieldsAndSelections();
        }

    }
}
