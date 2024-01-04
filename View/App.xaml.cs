using Model.Data;
using Model.Data.Interfaces;
using Model.Data.Repositories;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            HospitalContext context = new HospitalContext();
            IAppointmentRepo appointmentRepo = new AppointmentRepo(context);
            IAppointmentTimeRepo appointmentTimeRepo = new AppointmentTimeRepo(context);
            IDoctorRepo doctorRepo = new DoctorRepo(context);
            IPatientRepo patientRepo = new PatientRepo(context);
            ITypeDoctorRepo typeDoctorRepo = new TypeDoctorRepo(context);

            IRepository repository = new HospitalRepository(appointmentRepo, appointmentTimeRepo,
                                                            doctorRepo, patientRepo, typeDoctorRepo);
            HospitalModel hospitalModel = new HospitalModel(repository);
            HospitalViewModel hospitalViewModel = new HospitalViewModel(hospitalModel);
            var w = new MainWindow(hospitalViewModel);

            w.Show();
        }
    }
}
