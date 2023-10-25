﻿using Model.Data;
using System.Collections.ObjectModel;

namespace Model.Model
{
    public class HospitalModel : AbstractModel
    {
        public HospitalModel(IRepository repository)
        {
            _repository = repository;

            InitTypeDoctors();
            InitDoctors();
            InitAppointmentTimes();
        }

        private IRepository _repository;
        public ICollection<DoctorModel> DoctorModels { get; private set; }
        public ICollection<AppointmentTimeModel> AppointmentTimeModels { get; private set; }
        public ICollection<TypeDoctorModel> TypeDoctorModels { get; private set; }
        private void InitTypeDoctors()
        {
            TypeDoctorModels = _repository.GetTypeDoctorModels();

            if (TypeDoctorModels.Count == 0)
            {
                _repository.InitTypeDoctorModels();
                TypeDoctorModels = _repository.GetTypeDoctorModels();
            }
        }

        private void InitDoctors()
        {
            DoctorModels = _repository.GetDoctorModels();

            if (DoctorModels.Count == 0)
            {
                _repository.InitDoctors();
                DoctorModels = _repository.GetDoctorModels();
            }
        }

        public void InitAppointmentTimes()
        {
            AppointmentTimeModels = _repository.GetAppointmentTimes();

            if (AppointmentTimeModels.Count == 0)
            {
                _repository.InitAppointmentTimes();
                AppointmentTimeModels = _repository.GetAppointmentTimes();
            }
        }

        public string CallCreateAppointment(PatientModel? patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            if(patientModel != null)
            {
                CreateAppointment(patientModel, doctorModel, appointmentTimeModel);
                return "OK";
            }
            else
            {
                return "Error";
            }
        }

        public PatientModel? CreatePatient(string name, string surname)
        {
            if (PatientExists(name, surname))
            {
                return null;
            }
            else
            {
                _repository.CreatePatient(name, surname);
                return GetPatientModel(name, surname);
            }
        }

        public ICollection<AppointmentTimeModel> GetListFreeTimesDoctor(DoctorModel doctorModel)
        {
            return _repository.GetListFreeTimesDoctor(doctorModel);
        }

        private bool PatientExists(string name, string surname)
        {
            return _repository.PatientExists(name, surname);
        }

        private void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        { 
            _repository.CreateAppointment(patientModel, doctorModel, appointmentTimeModel);
        }

        private PatientModel GetPatientModel(string name, string surname)
        {
            return _repository.GetPatientModel(name, surname);
        }
    }
}
