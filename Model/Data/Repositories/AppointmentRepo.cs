using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Data.Interfaces;
using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.Repositories
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly HospitalContext _context;
        public AppointmentRepo(HospitalContext context) 
        {
            _context = context;
        }
        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            try
            {
                Appointment appointment = new Appointment()
                {
                    PatientId = patientModel.Id,
                    DoctorId = doctorModel.Id,
                    AppointmentTimeId  = appointmentTimeModel.Id
                };

                _context.ChangeTracker.Clear();
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public AppointmentModel? GetAppointment(int id)
        {
            try
            {
                Appointment appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
                TypeDoctorModel typeDoctorModel = new TypeDoctorModel(appointment.Doctor.Type.Id, appointment.Doctor.Type.Type);
                DoctorModel doctorModel = new DoctorModel(appointment.Doctor.Id, appointment.Doctor.Name, appointment.Doctor.Surname, typeDoctorModel);
                PatientModel patientModel = new PatientModel(appointment.Patient.Id, appointment.Patient.Name, appointment.Patient.Surname);
                AppointmentTimeModel appointmentTimeModel = new AppointmentTimeModel(appointment.AppointmentTime.Id, appointment.AppointmentTime.StartTime
                                                                , appointment.AppointmentTime.StartTime);

                return new AppointmentModel(id, doctorModel, patientModel, appointmentTimeModel); 
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ICollection<AppointmentModel> GetAppointmentModelsByDoctorId(int doctorId)
        {
            ICollection<AppointmentModel> appointmentModels = new List<AppointmentModel>();
            try
            {
                ICollection<Appointment> appointments = _context.Appointments
                                                        .Where(a => a.DoctorId == doctorId)
                                                        .Include(a => a.Doctor)
                                                        .Include (a => a.Patient)
                                                        .Include (a => a.AppointmentTime)
                                                        .ToList();

                foreach (var appointment in appointments)
                {
                    TypeDoctorModel typeDoctorModel = new TypeDoctorModel(appointment.Doctor.Type.Id, appointment.Doctor.Type.Type);
                    DoctorModel doctorModel = new DoctorModel(appointment.Doctor.Id, appointment.Doctor.Name, appointment.Doctor.Surname, typeDoctorModel);
                    PatientModel patientModel = new PatientModel(appointment.Patient.Id, appointment.Patient.Name, appointment.Patient.Surname);
                    AppointmentTimeModel appointmentTimeModel = new AppointmentTimeModel(appointment.AppointmentTime.Id, appointment.AppointmentTime.StartTime
                                                                    , appointment.AppointmentTime.StartTime);
                    appointmentModels.Add(new AppointmentModel(appointment.Id, doctorModel, patientModel, appointmentTimeModel));
                }

                return appointmentModels;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
