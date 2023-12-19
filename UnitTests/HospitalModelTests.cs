using Model.Data;
using Model.Data.Interfaces;
using Model.Data.Repositories;
using Model.EFModel;
using Model.Model;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class HospitalModelTests
    {
        private HospitalModel _hospitalModel;
        private Mock<IRepository> _repositoryMock;
        private List<DoctorModel> _doctorModels;
        private List<TypeDoctorModel> _typeDoctorModels;
        private List<AppointmentTimeModel> _appointmentTimeModels;
        private string _namePatient;
        private string _surnamePatient;
        AppointmentTimeModel _expectedAppointmentTimeModel;

        public HospitalModelTests()
        {
            _typeDoctorModels = new List<TypeDoctorModel>
            {
                new TypeDoctorModel(1,"��������")
            };
            _doctorModels = new List<DoctorModel>
            {
                new DoctorModel(1,"���������","������",_typeDoctorModels[0])
            };
            _expectedAppointmentTimeModel = new AppointmentTimeModel(2, new TimeSpan(10, 30, 00), new TimeSpan(11, 00, 00));
            _appointmentTimeModels = new List<AppointmentTimeModel>
            {
                new AppointmentTimeModel(1,new TimeSpan(10,00,00),new TimeSpan(10,30,00)),
                _expectedAppointmentTimeModel
            };

            _repositoryMock = new Mock<IRepository>();           

            _namePatient = "Peter";
            _surnamePatient = "Griffin";

            _repositoryMock.Setup(r => r.GetTypeDoctorModels()).Returns(_typeDoctorModels);
            _repositoryMock.Setup(r => r.InitTypeDoctorModels());
            _repositoryMock.Setup(r => r.GetDoctorModels()).Returns(_doctorModels);
            _repositoryMock.Setup(a => a.InitDoctors(_typeDoctorModels));
            _repositoryMock.Setup(a => a.GetAppointmentTimes()).Returns(_appointmentTimeModels);
            _repositoryMock.Setup(a => a.InitAppointmentTimes());
            

            _hospitalModel = new HospitalModel(_repositoryMock.Object);
        }

        [Fact]
        public void CreateAppointment()
        {
            // Arrange
            PatientModel patientModel = new PatientModel(1, "Peter", "Griffin"); 
            AppointmentTimeModel appointmentTimeModel = new AppointmentTimeModel(1, new TimeSpan(10, 30, 00),
                new TimeSpan(11, 00, 00));

            _repositoryMock.Setup(a => a.CreateAppointment(patientModel, _doctorModels[0],appointmentTimeModel));

            // Act
            string result = _hospitalModel.CallCreateAppointment(patientModel, _doctorModels[0], appointmentTimeModel);

            // Assert
            Assert.Equal("OK", result);

        }

        [Fact]
        public void CreatePatient()
        {
            // Arrange
            _repositoryMock.Setup(a => a.CreatePatient(_namePatient, _surnamePatient));
            _repositoryMock.Setup(a => a.GetPatientModel(_namePatient, _surnamePatient))
                           .Returns(new PatientModel(1, _namePatient, _surnamePatient));

            // Act
            PatientModel? patient = _hospitalModel.CreatePatient(_namePatient, _surnamePatient);

            // Assert
            Assert.NotNull(patient);
        }

        [Fact]
        public void GetListFreeTimesDoctor()
        {
            // Arrange
            PatientModel patientModel = new PatientModel(1, "����", "�������");
            ICollection<AppointmentModel> _appointmentModels = new List<AppointmentModel>()
            {
                new AppointmentModel(1,_doctorModels[0],patientModel,_appointmentTimeModels[0])
            };
            _repositoryMock.Setup(a => a.GetAppointmentModelsByDoctorId(_doctorModels[0].Id))
                                                           .Returns(_appointmentModels);
            ICollection<AppointmentTimeModel> proveListFreeTimeDoctor = new List<AppointmentTimeModel>()
            {
                _expectedAppointmentTimeModel
            };

            // Act
            ICollection<AppointmentTimeModel> listFreeTimesDoctor = _hospitalModel.GetListFreeTimesDoctor(_doctorModels[0]);

            AppointmentTimeModel actualAppointmentTimeModel = listFreeTimesDoctor.FirstOrDefault();
            AppointmentTimeModel expectedAppointmentTimeModel = _appointmentTimeModels.First(x => x.Id == 2);

            // Assert
            Assert.Equal(expectedAppointmentTimeModel,actualAppointmentTimeModel);
        }
    }
}