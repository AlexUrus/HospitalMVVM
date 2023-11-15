using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Data.Interfaces;
using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
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
                    Patient = ConverterModelToEF.Convert(patientModel),
                    Doctor = ConverterModelToEF.Convert(doctorModel),
                    AppointmentTime = ConverterModelToEF.Convert(appointmentTimeModel)
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
