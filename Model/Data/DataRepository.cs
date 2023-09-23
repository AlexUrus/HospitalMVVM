using Model.EFModel;
using Model.Model;
using System.Collections.ObjectModel;

namespace Model.Data
{
    public class DataRepository
    {
        private HospitalContext _context;
        private static DataRepository? _instance;

        public static DataRepository Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                {
                    _instance = new DataRepository();
                    return _instance;
                }

            }

        }

        private DataRepository()
        {
            _context = new HospitalContext();
        }

        public void SetAppointment(AppointmentModel appointmentModel)
        {
            try
            {
                Appointment appointment = ConvertModelToEf(appointmentModel);
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }
        public bool PatientExists(string name, string surname)
        {
            try
            {
                bool exists = _context.Patients.Any(p => p.Name == name && p.Surname == surname);
                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking patient existence: {ex.Message}");
                throw;
            }
        }

        public bool IsFreeDoctorInTime(AppointmentTimeModel appointmentTimeModel, DoctorModel doctorModel)
        {
            try
            {
                Appointment? appointment = _context.Appointments
                    .FirstOrDefault(a => a.AppointmentTime == ConvertModelToEf(appointmentTimeModel) && a.Doctor == ConvertModelToEf(doctorModel));
                return appointment == null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AppointmentTimeModel> GetListFreeTimesDoctor(DoctorModel doctorModel)
        {
            try
            {
                List<Appointment> appointments = _context.Appointments
                                            .Where(a => a.Doctor == ConvertModelToEf(doctorModel)).Distinct().ToList();

                List<AppointmentTimeModel> appointmentTimeModels = new List<AppointmentTimeModel>();
                foreach (Appointment appointment in appointments)
                {
                    appointmentTimeModels.Add(
                        new AppointmentTimeModel(appointment.AppointmentTime.Id,
                        appointment.AppointmentTime.StartTime, appointment.AppointmentTime.EndTime));
                }

                return appointmentTimeModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking doctor existence: {ex.Message}");
                throw;
            }
        }

        public ObservableCollection<AppointmentModel> GetAppointmentList()
        {
            ObservableCollection<AppointmentModel> appointmentModels = new ObservableCollection<AppointmentModel>();
            try
            {
                List<Appointment> appointmentlist = _context.Appointments.ToList();
                foreach (Appointment appointment in appointmentlist)
                {
                    appointmentModels.Add(new AppointmentModel
                    {
                        AppointmentTimeModel = new AppointmentTimeModel(appointment.AppointmentTime.Id, appointment.AppointmentTime.StartTime, appointment.AppointmentTime.EndTime),
                        DoctorModel = new DoctorModel(appointment.Doctor.Id, appointment.Doctor.Name, appointment.Doctor.Surname, appointment.Doctor.Type),
                        PatientModel = new PatientModel(appointment.Patient.Id, appointment.Patient.Name, appointment.Patient.Surname)
                    });
                }
                return appointmentModels;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ObservableCollection<DoctorModel> GetDoctors()
        {
            ObservableCollection<DoctorModel> doctorModels;
            try
            {
                List<Doctor> listDoctor = _context.Doctors.ToList();
                doctorModels = new ObservableCollection<DoctorModel>();
                foreach (Doctor doctor in listDoctor)
                {
                    doctorModels.Add(new DoctorModel(doctor.Id, doctor.Name, doctor.Surname, doctor.Type));
                }

                return doctorModels;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ObservableCollection<AppointmentTimeModel> GetAppointmentTimes()
        {
            ObservableCollection<AppointmentTimeModel> appointmentTimeModels;
            try
            {
                List<AppointmentTime> listAppointmentTimes = _context.AppointmentTimes.ToList();
                appointmentTimeModels = new ObservableCollection<AppointmentTimeModel>();
                foreach (AppointmentTime appointmentTime in listAppointmentTimes)
                {
                    appointmentTimeModels.Add(new AppointmentTimeModel(appointmentTime.Id, appointmentTime.StartTime, appointmentTime.EndTime));
                }
                return appointmentTimeModels;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreateDoctors()
        {
            List<Doctor> doctors = new List<Doctor>()
            {
                    new Doctor()
                    {
                        Name = "Иван",
                        Surname = "Иванов",
                        Type = new TypeDoctor{ Type = "Терапевт"}
                    },
                    new Doctor()
                    {
                        Name = "Михаил",
                        Surname = "Михайлов",
                        Type = new TypeDoctor{ Type = "Дерматолог"}
                    },
                    new Doctor()
                    {
                        Name = "Петр",
                        Surname = "Петров",
                        Type = new TypeDoctor { Type = "ЛОР" }
                    },
                    new Doctor()
                    {
                        Name = "Сидр",
                        Surname = "Сидоров",
                        Type = new TypeDoctor { Type = "Психолог" }
                    },
                    new Doctor()
                    {
                        Name = "Александр",
                        Surname = "Александров",
                        Type = new TypeDoctor { Type = "Гастроэнтеролог" }
                    }
            };

            _context.Doctors.AddRange(doctors);
            _context.SaveChanges();
        }

        public void CreateAppointmentTimes()
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
            Doctor doctor = new Doctor()
            {
                Surname = doctorModel.Surname,
                Name = doctorModel.Name
            };
            return doctor;
        }

        public AppointmentTime ConvertModelToEf(AppointmentTimeModel appointmentTimeModel)
        {
            AppointmentTime appointmentTime = new AppointmentTime()
            {
                StartTime = appointmentTimeModel.StartTime,
                EndTime = appointmentTimeModel.EndTime
            };
            return appointmentTime;
        }

        public Appointment ConvertModelToEf(AppointmentModel appointmentModel)
        {
            Appointment appointment = new Appointment()
            {
                AppointmentTime = ConvertModelToEf(appointmentModel.AppointmentTimeModel),
                Doctor = ConvertModelToEf(appointmentModel.DoctorModel),
                Patient = ConvertModelToEf(appointmentModel.PatientModel)
            };
            return appointment;
        }
    }
}
