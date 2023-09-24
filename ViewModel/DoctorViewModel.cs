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
        private DoctorModel? _selectedDoctor;
        private ObservableCollection<AppointmentTimeModel> _listTakenTimesDoctor;
        public DoctorModel? SelectedDoctor
        {
            get => _selectedDoctor;
            set 
            {
                this.RaiseAndSetIfChanged(ref _selectedDoctor, value);
                UpdateListTakenTimesDoctor();
            }
        }

        public ObservableCollection<AppointmentTimeModel> ListTakenTimesDoctor
        {
            get
            {
                return _listTakenTimesDoctor;
            }
            private set
            {
                this.RaiseAndSetIfChanged(ref _listTakenTimesDoctor, value);
            }
        }

        public bool IsFreeDoctorInTime(AppointmentTimeModel appointmentTimeModel)
        {
            return _selectedDoctor.IsFreeDoctorInTime(appointmentTimeModel);
        }

        public ObservableCollection<AppointmentTimeModel> GetFreeTimeDoctor()
        {
            return _selectedDoctor.GetFreeTimeDoctor();
        }

        private void UpdateListTakenTimesDoctor()
        {
            ListTakenTimesDoctor = SelectedDoctor?.ListFreeTimesDoctor;
        }


    }

}
