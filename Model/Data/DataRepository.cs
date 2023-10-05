using Microsoft.Data.SqlClient;
using Model.EFModel;
using Model.Model;
using System;
using System.Collections.ObjectModel;

namespace Model.Data
{
    public class DataRepository
    {
        private HospitalContext _context;

        public DataRepository()
        {
            _context = new HospitalContext();
        }

        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            try
            { 
                Appointment appointment = new Appointment() {Patient = ConvertModelToEf(patientModel), 
                    Doctor = ConvertModelToEf(doctorModel), AppointmentTime = ConvertModelToEf(appointmentTimeModel) };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public AppointmentModel GetAppointmentModel(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            try
            {
                Patient patient = ConvertModelToEf(patientModel);
                Doctor doctor = ConvertModelToEf(doctorModel);
                AppointmentTime appointmentTime = ConvertModelToEf(appointmentTimeModel);

                Appointment appointment = _context.Appointments.FirstOrDefault(p => p.Patient == patient && p.Doctor == doctor && p.AppointmentTime == appointmentTime);

                return new AppointmentModel(appointment.Id, doctorModel, patientModel, appointmentTimeModel);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void CreatePatient(string name, string surname)
        {
            try
            {
                Patient patient = new Patient() { Name = name, Surname = surname };
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool PatientExists(string name, string surname)
        {
            try
            {
                bool exists = _context.Patients.Any(p => p.Name == name && p.Surname == surname);
                return exists;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public PatientModel GetPatientModel(string name, string surname)
        {
            try
            {
                Patient patient = _context.Patients.FirstOrDefault(p => p.Name == name && p.Surname == surname);
                return new PatientModel(patient.Id, patient.Name, patient.Surname);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void CreateDoctors()
        {
            try
            {
                ICollection<TypeDoctor> typeDoctors = GetTypeDoctors();
                ICollection<Doctor> doctors = new List<Doctor>()
                {
                    new Doctor()
                    {
                        Name = "Иван",
                        Surname = "Иванов",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Терапевт")
                    },
                    new Doctor()
                    {
                        Name = "Михаил",
                        Surname = "Михайлов",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Дерматолог")
                    },
                    new Doctor()
                    {
                        Name = "Петр",
                        Surname = "Петров",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "ЛОР")
                    },
                    new Doctor()
                    {
                        Name = "Сидр",
                        Surname = "Сидоров",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Психолог")
                    },
                    new Doctor()
                    {
                        Name = "Александр",
                        Surname = "Александров",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Гастроэнтеролог")
                    }
                };

                _context.Doctors.AddRange(doctors);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ICollection<AppointmentTimeModel> GetListFreeTimesDoctor(DoctorModel doctorModel)
        {
            try
            {
                ICollection<AppointmentTimeModel> allAppointmentTimes = GetAppointmentTimes();

                var busyTimes = _context.Appointments
                    .Where(a => a.Doctor == ConvertModelToEf(doctorModel))
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

        public ICollection<DoctorModel> GetDoctorModels()
        {
            ICollection<DoctorModel> doctorModels;
            try
            {
                ICollection<Doctor> listDoctor = _context.Doctors.ToList();
                doctorModels = new List<DoctorModel>();

                foreach (Doctor doctor in listDoctor)
                {
                    doctorModels.Add(new DoctorModel(doctor.Id, doctor.Name, doctor.Surname, new TypeDoctorModel(doctor.Type.Id, doctor.Type.Type)));
                }

                return doctorModels;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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

        public void CreateAppointmentTimes()
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

        public void CreateTypeDoctorModels()
        {
            try
            {
                List<TypeDoctor> typeDoctors = new List<TypeDoctor>()
                {
                new TypeDoctor()
                {
                    Type = "Терапевт"
                },
                new TypeDoctor()
                {
                    Type = "Дерматолог"
                },
                new TypeDoctor()
                {
                    Type = "ЛОР"
                },
                new TypeDoctor()
                {
                    Type = "Психолог"
                },
                new TypeDoctor()
                {
                    Type = "Гастроэнтеролог"
                },
                };

                _context.TypeDoctors.AddRange(typeDoctors);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }
        private ICollection<TypeDoctor> GetTypeDoctors()
        {
            try
            { 
                return _context.TypeDoctors.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public ICollection<TypeDoctorModel> GetTypeDoctorModels()
        {
            ICollection<TypeDoctorModel> typeDoctorModels;
            try
            {
                ICollection<TypeDoctor> listTypeDoctors = _context.TypeDoctors.ToList();
                typeDoctorModels = new List<TypeDoctorModel>();

                foreach (TypeDoctor typeDoctor in listTypeDoctors)
                {
                    typeDoctorModels.Add(new TypeDoctorModel(typeDoctor.Id, typeDoctor.Type));
                }

                return typeDoctorModels;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #region ConvertModelToEf
        public Patient ConvertModelToEf(PatientModel patientModel)
        {
            Patient patient = new Patient()
            {
                Surname = patientModel.Surname,
                Name = patientModel.Name
            };
            return patient;
        }

        public Doctor ConvertModelToEf(DoctorModel doctorModel)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(d => d.Id == doctorModel.Id);
            return doctor;
        }

        public AppointmentTime ConvertModelToEf(AppointmentTimeModel appointmentTimeModel)
        {
            AppointmentTime appointmentTime = _context.AppointmentTimes.FirstOrDefault(a => a.Id == appointmentTimeModel.Id);
            return appointmentTime;
        }

        public Appointment ConvertModelToEf(AppointmentModel appointmentModel)
        {
            Appointment appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentModel.Id);
            return appointment;
        }

        public TypeDoctor ConvertModelToEf(TypeDoctorModel typeDoctorModel)
        {
            TypeDoctor typeDoctor = _context.TypeDoctors.FirstOrDefault(a => a.Id == typeDoctorModel.Id);
            return typeDoctor;
        }
        #endregion
    }
}
