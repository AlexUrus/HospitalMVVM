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
    public class AppointmentTimeRepo : IAppointmentTimeRepo
    {
        private readonly HospitalContext _context;
        public AppointmentTimeRepo(HospitalContext context)
        {
            _context = context;
        }
        public ICollection<AppointmentTimeModel> GetAppointmentTimes()
        {
            ICollection<AppointmentTimeModel> appointmentTimeModels;
            try
            {
                ICollection<AppointmentTime> listAppointmentTimes = _context.AppointmentTimes.ToList();
                appointmentTimeModels = new List<AppointmentTimeModel>();
                foreach (AppointmentTime appointmentTime in listAppointmentTimes)
                {
                    appointmentTimeModels.Add(new AppointmentTimeModel(appointmentTime.Id, appointmentTime.StartTime, appointmentTime.EndTime));
                }
                return appointmentTimeModels;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ICollection<AppointmentTimeModel> GetListFreeTimesDoctor(DoctorModel doctorModel)
        {
            try
            {
                ICollection<AppointmentTimeModel> allAppointmentTimes = GetAppointmentTimes();

                var busyTimes = _context.Appointments
                    .Where(a => a.Doctor == ConverterModelToEF.Convert(doctorModel))
                    .Select(a => a.AppointmentTime.Id)
                    .ToList();

                var freeTimes = allAppointmentTimes
                    .Where(time => !busyTimes.Contains(time.Id))
                    .ToList();

                return freeTimes;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InitAppointmentTimes()
        {
            try
            {
                List<AppointmentTime> appointmentTimes = new List<AppointmentTime>()
            {
                new AppointmentTime()
                {
                    StartTime = new TimeSpan(10,00,00),
                    EndTime = new TimeSpan(10,30,00),
                },
                new AppointmentTime()
                {
                    StartTime = new TimeSpan(10,30,00),
                    EndTime = new TimeSpan(11,00,00),
                },
                new AppointmentTime()
                {
                    StartTime = new TimeSpan(11,00,00),
                    EndTime = new TimeSpan(11,30,00),
                },
                new AppointmentTime()
                {
                    StartTime = new TimeSpan(11,30,00),
                    EndTime = new TimeSpan(12,00,00),
                },
            };

                _context.AppointmentTimes.AddRange(appointmentTimes);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
