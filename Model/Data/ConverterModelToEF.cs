using Microsoft.EntityFrameworkCore;
using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public static class ConverterModelToEF
    {
        public static Patient Convert(PatientModel patientModel)
        {
            Patient patient = new Patient()
            {
                Id = patientModel.Id,
                Surname = patientModel.Surname,
                Name = patientModel.Name
            };
            return patient;
        }

        public static Doctor Convert(DoctorModel doctorModel)
        {
            Doctor doctor = new Doctor() 
            {
                Id = doctorModel.Id,
                Name = doctorModel.Name,
                Surname = doctorModel.Surname,
                Type = Convert(doctorModel.Type)
            };
            return doctor;
        }

        public static AppointmentTime Convert(AppointmentTimeModel appointmentTimeModel)
        {
            AppointmentTime appointmentTime = new AppointmentTime()
            {
                Id = appointmentTimeModel.Id,
                StartTime = appointmentTimeModel.StartTime,
                EndTime = appointmentTimeModel.EndTime
            };
            return appointmentTime;
        }

        public static Appointment Convert(AppointmentModel appointmentModel)
        {
            Appointment appointment = new Appointment()
            {
                Id = appointmentModel.Id,
                Doctor = Convert(appointmentModel.DoctorModel),
                Patient = Convert(appointmentModel.PatientModel),
                AppointmentTime = Convert(appointmentModel.AppointmentTimeModel)
            };
            return appointment;
        }

        public static TypeDoctor Convert(TypeDoctorModel typeDoctorModel)
        {
            TypeDoctor typeDoctor = new TypeDoctor()
            {
                Id = typeDoctorModel.Id,
                Type = typeDoctorModel.Type,
            };
            return typeDoctor;
        }
    }
}
