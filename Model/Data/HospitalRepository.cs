using Microsoft.Data.SqlClient;
using Model.Data.Interfaces;
using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model.Data
{
    public class HospitalRepository : IRepository
    { 
        private IAppointmentRepo _appointmentRepo;
        private IAppointmentTimeRepo _appointmentTimeRepo;
        private IDoctorRepo _doctorRepo;
        private IPatientRepo _patientRepo;
        private ITypeDoctorRepo _typeDoctorRepo;

        public HospitalRepository(IAppointmentRepo appointmentRepo, IAppointmentTimeRepo appointmentTimeRepo, IDoctorRepo doctorRepo, IPatientRepo patientRepo, ITypeDoctorRepo typeDoctorRepo)
        {
            _appointmentRepo = appointmentRepo;
            _appointmentTimeRepo = appointmentTimeRepo;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _typeDoctorRepo = typeDoctorRepo;
        }

        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            _appointmentRepo.CreateAppointment(patientModel, doctorModel, appointmentTimeModel);
        }

        public void CreatePatient(string name, string surname)
        {
            _patientRepo.CreatePatient(name, surname);
        }

        public bool PatientExists(string name, string surname)
        {
            return _patientRepo.PatientExists(name, surname);
        }

        public PatientModel GetPatientModel(string name, string surname)
        {
            return _patientRepo.GetPatientModel(name, surname);
        }

        public void InitDoctors(ICollection<TypeDoctorModel> typeDoctors)
        {
            _doctorRepo.InitDoctors(typeDoctors);
        }

        public ICollection<DoctorModel> GetDoctorModels()
        {
            return _doctorRepo.GetDoctorModels();
        }

        public ICollection<AppointmentTimeModel> GetAppointmentTimes()
        {
            return _appointmentTimeRepo.GetAppointmentTimes();
        }

        public void InitAppointmentTimes()
        {
            _appointmentTimeRepo.InitAppointmentTimes();
        }

        public void InitTypeDoctorModels()
        {
            _typeDoctorRepo.InitTypeDoctorModels();
        }

        public ICollection<TypeDoctorModel> GetTypeDoctorModels()
        {
            return _typeDoctorRepo.GetTypeDoctorModels();
        }

        public AppointmentModel? GetAppointmentModel(int id)
        {
            return _appointmentRepo.GetAppointment(id);
        }

        public ICollection<AppointmentModel> GetAppointmentModelsByDoctorId(int doctorId)
        {
            return _appointmentRepo.GetAppointmentModelsByDoctorId(doctorId);
        }
    }
}
