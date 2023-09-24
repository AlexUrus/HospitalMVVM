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

        public void CreateAppointment(PatientModel patientModel, DoctorModel doctorModel, AppointmentTimeModel appointmentTimeModel)
        {
            try
            { 
                Appointment appointment = new Appointment() {Patient = ConvertModelToEf(patientModel), 
                    Doctor = ConvertModelToEf(doctorModel), AppointmentTime = ConvertModelToEf(appointmentTimeModel) };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception ex)
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

        public PatientModel GetPatientModel(string name, string surname)
        {
            try
            {
                Patient patient = _context.Patients.FirstOrDefault(p => p.Name == name && p.Surname == surname);
                return new PatientModel(patient.Id, patient.Name, patient.Surname);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CreateDoctors()
        {
            List<TypeDoctor> typeDoctors = GetTypeDoctors();
            List<Doctor> doctors = new List<Doctor>()
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

        public DoctorModel GetDoctorModel(string name, string surname)
        {
            try
            {
                Doctor doctor = _context.Doctors.FirstOrDefault(p => p.Name == name && p.Surname == surname);
                return new DoctorModel(doctor.Id, doctor.Name, doctor.Surname, new TypeDoctorModel(doctor.Type.Id, doctor.Type.Type));
            }
            catch (Exception ex)
            {
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

        public ObservableCollection<AppointmentTimeModel> GetListFreeTimesDoctor(DoctorModel doctorModel)
        {
            try
            {
                ObservableCollection<AppointmentTimeModel> allAppointmentTimes = GetAppointmentTimes();

                var busyTimes = _context.Appointments
                    .Where(a => a.Doctor == ConvertModelToEf(doctorModel))
                    .Select(a => a.AppointmentTime.Id)
                    .ToList();

                var freeTimes = allAppointmentTimes
                    .Where(time => !busyTimes.Contains(time.Id))
                    .ToList();

                return new ObservableCollection<AppointmentTimeModel>(freeTimes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking doctor existence: {ex.Message}");
                throw;
            }
        }

        public ObservableCollection<DoctorModel> GetDoctorModels()
        {
            ObservableCollection<DoctorModel> doctorModels;
            try
            {
                List<Doctor> listDoctor = _context.Doctors.ToList();
                doctorModels = new ObservableCollection<DoctorModel>();

                foreach (Doctor doctor in listDoctor)
                {
                    doctorModels.Add(new DoctorModel(doctor.Id, doctor.Name, doctor.Surname, new TypeDoctorModel(doctor.Type.Id, doctor.Type.Type)));
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

        public void CreateTypeDoctorModels()
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
        private List<TypeDoctor> GetTypeDoctors()
        {
            try
            { 
                return _context.TypeDoctors.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ObservableCollection<TypeDoctorModel> GetTypeDoctorModels()
        {
            ObservableCollection<TypeDoctorModel> typeDoctorModels;
            try
            {
                List<TypeDoctor> listTypeDoctors = _context.TypeDoctors.ToList();
                typeDoctorModels = new ObservableCollection<TypeDoctorModel>();

                foreach (TypeDoctor typeDoctor in listTypeDoctors)
                {
                    typeDoctorModels.Add(new TypeDoctorModel(typeDoctor.Id, typeDoctor.Type));
                }

                return typeDoctorModels;
            }
            catch (Exception)
            {

                throw;
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
