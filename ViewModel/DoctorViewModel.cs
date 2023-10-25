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
    public class DoctorViewModel : BaseViewModel
    { 
        private DoctorModel? _doctor;
        private string _doctorFIO;
        private ObservableCollection<AppointmentTimeModel> _listFreeTimesDoctor;
        
        public string DoctorFIO
        {
            get => _doctorFIO;
            set
            {
                this.RaiseAndSetIfChanged(ref _doctorFIO, value);

            }
        }
        public DoctorModel? Doctor
        {
            get => _doctor;
            set 
            {
                this.RaiseAndSetIfChanged(ref _doctor, value);
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

        public void ClearFields()
        {
            Doctor = null;
            ListFreeTimesDoctor.Clear();
        }

        public bool IsCanCreateAppointment()
        {
            return Doctor != null;
        }
    }
}
