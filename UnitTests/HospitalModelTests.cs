using Model.Data;
using Model.Model;
using Xunit;

namespace UnitTests
{
    public class HospitalModelTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange
            IRepository repository = new HospitalRepository();
            HospitalModel hospitalModel = new HospitalModel(repository);

            PatientModel patientModel = new PatientModel(1,"Peter","Griffin");
            DoctorModel doctorModel = new DoctorModel(1, "Иван", "Сидоров", new TypeDoctorModel(1, "Диетолог"));
            AppointmentTimeModel appointmentTimeModel = new AppointmentTimeModel(1, new TimeSpan(10, 30, 00),
                new TimeSpan(11, 00, 00));

            // Act
            string result = hospitalModel.CallCreateAppointment(patientModel, doctorModel, appointmentTimeModel);

            // Assert
            Assert.Equal("OK", result);
        }

    }
}