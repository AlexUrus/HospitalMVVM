using Model.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class DoctorViewModel : ReactiveObject
    {
        private ObservableCollection<DoctorModel> _doctors;
        private DoctorModel? _selectedDoctor;

        public ObservableCollection<DoctorModel> Doctors
        {
            get => _doctors;
            set => this.RaiseAndSetIfChanged(ref _doctors, value);
        }

        public DoctorModel? SelectedDoctor
        {
            get => _selectedDoctor;
            set => this.RaiseAndSetIfChanged(ref _selectedDoctor, value);
        }

        public bool IsFreeDoctorInTime(AppointmentTimeModel appointmentTimeModel)
        {
            return _selectedDoctor.IsFreeDoctorInTime(appointmentTimeModel);
        }

        public List<AppointmentTimeModel> GetFreeTimeDoctor()
        {
            return _selectedDoctor.GetFreeTimeDoctor();
        }
    }

}
