using Model.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;


namespace ViewModel
{
    public class HospitalViewModel : ReactiveObject
    {
        #region Fields
        private HospitalModel _hospitalModel { get; set; }

        private PatientViewModel _patientViewModel;
        private DoctorViewModel _doctorViewModel;
        private AppointmentTimeViewModel _appointmentTimeViewModel;
        private MessageViewModel _messageViewModel;

        private bool _canCreateAppointment;
        #endregion

        #region Properties
        public ObservableCollection<DoctorModel> DoctorModels { get; set; }
        public ObservableCollection<AppointmentTimeModel> AppointmentTimeModels { get; set; }
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

        public MessageViewModel MessgeViewModel
        {
            get => _messageViewModel;
            private set => this.RaiseAndSetIfChanged(ref _messageViewModel, value);
        }

        public bool CanCreateAppointment
        {
            get => _canCreateAppointment;
            private set => this.RaiseAndSetIfChanged(ref _canCreateAppointment, value);
        }
        #endregion

        #region Commands
        private ICommand _openMessageWindow;
        public ICommand OpenMessageWindow
        {
            get
            {
                if (_openMessageWindow == null)
                {
                    //_openMessageWindow = new OpenMessageWindowCommand(this);
                }
                return _openMessageWindow;
            }
        }
        public ReactiveCommand<Unit, Unit> CreateAppointmentCommand { get; }
        #endregion
        public HospitalViewModel()
        {  
            InitViewModels();
            InitModels();
            InitAndSubscribeUpdateCanCreate();

            CreateAppointmentCommand = ReactiveCommand.Create(CallCreateAppointment);
        }

        private void InitViewModels()
        {
            _patientViewModel = new PatientViewModel();
            _doctorViewModel = new DoctorViewModel();
            _appointmentTimeViewModel = new AppointmentTimeViewModel();
            _messageViewModel = new MessageViewModel();
        }

        private void InitModels()
        {
            _hospitalModel = HospitalModel.Instance;
            AppointmentTimeModels = new ObservableCollection<AppointmentTimeModel>( _hospitalModel.AppointmentTimeModels);
            DoctorModels = new ObservableCollection<DoctorModel>( _hospitalModel.DoctorModels);
        }

        private void InitAndSubscribeUpdateCanCreate()
        {
            var combinedPropertiesUpdateCanCreate = this.WhenAnyValue(
               x => x._patientViewModel.PatientName,
               x => x._patientViewModel.PatientSurname,
               x => x._doctorViewModel.SelectedDoctor,
               x => x._appointmentTimeViewModel.SelectedAppointmentTimeModel);

            combinedPropertiesUpdateCanCreate
                .Subscribe(_ => UpdateCanCreateAppointment());
        }

        private void CallCreateAppointment()
        {
            if (PatientExist())
            {
                CreatePatient();
                CreateAppointment();
            }
            else
            {
                // вывод сообщения Пациент существует
            }

            ClearInputFieldsAndSelections();
        }

        private void CreateAppointment()
        {
            PatientModel patientModel = _hospitalModel.GetPatientModel(_patientViewModel.PatientName, _patientViewModel.PatientSurname);
            DoctorModel doctorModel = _doctorViewModel.SelectedDoctor;
            AppointmentTimeModel appointmentTimeModel = _appointmentTimeViewModel.SelectedAppointmentTimeModel;

            _hospitalModel.CreateAppointment(patientModel, doctorModel, appointmentTimeModel);
        }

        private void CreatePatient()
        {
            _hospitalModel.CreatePatient(_patientViewModel.PatientName, _patientViewModel.PatientSurname);
        }

        private bool PatientExist()
        {
            return _hospitalModel.PatientExists(_patientViewModel.PatientName, _patientViewModel.PatientSurname);
        }

        private void UpdateCanCreateAppointment()
        {
            CanCreateAppointment = _appointmentTimeViewModel.SelectedAppointmentTimeModel is not null
                && _doctorViewModel.SelectedDoctor is not null
                && !string.IsNullOrWhiteSpace(_patientViewModel.PatientName)
                && !string.IsNullOrWhiteSpace(_patientViewModel.PatientSurname);
        }

        private void ClearInputFieldsAndSelections()
        {
            _patientViewModel.PatientName = "";
            _patientViewModel.PatientSurname = "";
            _doctorViewModel.SelectedDoctor = null;
            _appointmentTimeViewModel.SelectedAppointmentTimeModel = null;
        }
    }
}
