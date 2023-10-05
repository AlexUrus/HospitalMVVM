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
        private ObservableCollection<AppointmentTimeModel> _listFreeTimesDoctor;
        public DoctorModel? SelectedDoctor
        {
            get => _selectedDoctor;
            set 
            {
                this.RaiseAndSetIfChanged(ref _selectedDoctor, value);
            }
        }

        public ObservableCollection<AppointmentTimeModel> ListFreeTimesDoctor
        {
            get
            {
                return _listFreeTimesDoctor;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _listFreeTimesDoctor, value);
            }
        }
        /*
        private void UpdateListTakenTimesDoctor()
        {
            if(SelectedDoctor != null)
            {
                ListFreeTimesDoctor = new ObservableCollection<AppointmentTimeModel>(SelectedDoctor.ListFreeTimesDoctor);
            }
            else
            {
                ListFreeTimesDoctor = new ObservableCollection<AppointmentTimeModel>();
            }
        }*/
    }
}
