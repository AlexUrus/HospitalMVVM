using Model.Model;
using ReactiveUI;


namespace ViewModel
{
    public class AppointmentTimeViewModel : BaseViewModel
    {
        private AppointmentTimeModel? _appointmentTimeModel;

        public AppointmentTimeModel? AppointmentTimeModel
        {
            get => _appointmentTimeModel;
            set => this.RaiseAndSetIfChanged(ref _appointmentTimeModel, value);
        } 

        public void ClearFields()
        {
            AppointmentTimeModel = null;
        }
        public bool IsCanCreateAppointment()
        {
            return AppointmentTimeModel != null;
        }
    }
}
