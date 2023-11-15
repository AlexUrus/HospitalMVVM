using Model.Data;
using Model.Model;
using System;
using Xunit;

namespace UnitTests
{
    public class HospitalModelTests
    {
        private IRepository _repository;
        private HospitalModel _hospitalModel;
        public HospitalModelTests()
        {
            _repository = new HospitalRepository();
            _hospitalModel = new HospitalModel(_repository);
        }
         
        [Fact]
        public void CallCreateAppointmentTest()
        {
            // Arrange
            PatientModel patientModel = new PatientModel(1, "Peter", "Griffin"); 
            DoctorModel doctorModel = new DoctorModel(1, "Иван", "Сидоров", new TypeDoctorModel(1, "Диетолог"));
            AppointmentTimeModel appointmentTimeModel = new AppointmentTimeModel(1, new TimeSpan(10, 30, 00),
                new TimeSpan(11, 00, 00));

            // Act
            string result = _hospitalModel.CallCreateAppointment(patientModel, doctorModel, appointmentTimeModel);

            // Assert
            Assert.Equal("OK", result);

            // Act
            result = _hospitalModel.CallCreateAppointment(null, doctorModel, appointmentTimeModel);

            // Assert
            Assert.Equal("Error", result);

        }

        [Fact]
        public void CreatePatientTest()
        {
            // Arrange
            Random random = new Random();

            string name = "Bart" + random.Next();
            string surname = "Griffin" + random.Next();

            // Act
            PatientModel? patient = _hospitalModel.CreatePatient(name, surname);

            // Assert
            Assert.NotNull(patient);

            // Act
            patient = _hospitalModel.CreatePatient(name, surname);

            // Assert
            Assert.Null(patient);
        }

        [Fact]
        public void GetListFreeTimesDoctorTest()
        {
            // Arrange
            DoctorModel doctorModel = new DoctorModel(4, "Петр", "Сидоров", new TypeDoctorModel(2, "Хирург"));

            // Act
            ICollection<AppointmentTimeModel> listFreeTimesDoctor = _hospitalModel.GetListFreeTimesDoctor(doctorModel);

            // Assert
            Assert.NotEmpty(listFreeTimesDoctor);
        }
    }
}