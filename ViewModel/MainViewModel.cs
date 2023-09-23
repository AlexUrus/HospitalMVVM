using Model.Model;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;


namespace ViewModel
{
    public class MainViewModel : ReactiveObject
    {

        #region Fields
        private PatientViewModel _patientViewModel;
        private DoctorViewModel _doctorViewModel;
        private AppointmentTimeViewModel _appointmentTimeViewModel;
        private AppointmentViewModel _appointmentViewModel;
        private bool _canCreateAppointment;

        private HospitalModel _hospitalModel;
        #endregion

        #region Properties
        public PatientViewModel PatientViewModel
        {
            get => _patientViewModel;
            private set => this.RaiseAndSetIfChanged(ref _patientViewModel, value);
        }
        public DoctorViewModel DoctorViewModel
        {
            get => _doctorViewModel;
            private set => this.RaiseAndSetIfChanged(ref _doctorViewModel, value);
        }
        public AppointmentTimeViewModel AppointmentTimeViewModel
        {
            get => _appointmentTimeViewModel;
            private set => this.RaiseAndSetIfChanged(ref _appointmentTimeViewModel, value);
        }
        public bool CanCreateAppointment
        {
            get => _canCreateAppointment;
            private set => this.RaiseAndSetIfChanged(ref _canCreateAppointment, value);
        }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, Unit> CreateAppointmentCommand { get; }
        #endregion

        public MainViewModel()
        {
            _hospitalModel = HospitalModel.Instance;

            _patientViewModel = new PatientViewModel();
            _doctorViewModel = new DoctorViewModel();
            _appointmentTimeViewModel = new AppointmentTimeViewModel();
            _appointmentViewModel = new AppointmentViewModel();

            CreateAppointmentCommand = ReactiveCommand.Create(CreateAppointment);
            // Создаем реактивную последовательность для отслеживания изменений
            var combinedPropertiesUpdateCanCreate = this.WhenAnyValue(
                x => x._patientViewModel.PatientName,
                x => x._patientViewModel.PatientSurname,
                x => x._doctorViewModel.SelectedDoctor,
                x => x._appointmentTimeViewModel.SelectedAppointmentTimeModel);

            // Подписываемся на изменения свойств и вызываем UpdateCanCreateAppointment
            combinedPropertiesUpdateCanCreate
                .Subscribe(_ => UpdateCanCreateAppointment());

            var combinedPropertiesUpdateAppTime = this.WhenAnyValue(
            x => x._doctorViewModel.SelectedDoctor);

            // Подписываемся на изменения свойств и вызываем UpdateCanCreateAppointment
            combinedPropertiesUpdateAppTime
                .Subscribe(_ => UpdateAppointmentsTimeDoctor());
        }

        private void UpdateCanCreateAppointment()
        {
            CanCreateAppointment = _appointmentTimeViewModel.SelectedAppointmentTimeModel is not null
                && _doctorViewModel.SelectedDoctor is not null
                && !string.IsNullOrWhiteSpace(_patientViewModel.PatientName)
                && !string.IsNullOrWhiteSpace(_patientViewModel.PatientSurname);
        }

        public void CreateAppointment()
        {
            _hospitalModel.CreateAppointment();
        }

        private void ClearInputFieldsAndSelections()
        {
            _patientViewModel.PatientName = "";
            _patientViewModel.PatientSurname = "";
            _doctorViewModel.SelectedDoctor = null;
            _appointmentTimeViewModel.SelectedAppointmentTimeModel = null;
        }

        private void CreatePatient()
        {
            _hospitalModel.CreatePatient(_patientViewModel.PatientName,_patientViewModel.PatientSurname);
        }

        private void UpdateAppointmentsTimeDoctor()
        {
            /*
            if (_doctorViewModel.SelectedDoctor is not null)
            {
                _appointmentTimeViewModel.DublicateAllAppointmentTime();
                _appointmentTimeViewModel.RemoveBookedTimes(_doctorViewModel.GetFreeTimeDoctor());
            }*/
        }




    }
}
