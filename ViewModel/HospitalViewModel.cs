using Model.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;


namespace ViewModel
{
    public class HospitalViewModel : ReactiveObject
    {
        #region Fields
        private HospitalModel _hospitalModel;
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

        public MessageViewModel MessageViewModel
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
        public ReactiveCommand<Unit, Unit> CreateAppointmentCommand { get; }
        #endregion

        #region Constructors
        public HospitalViewModel()
        {  
            InitViewModels();
            InitModels();
            InitAndSubscribeUpdateCanCreate();
            InitAndSubscribeUpdateListTakenTimesDoctor();

            CreateAppointmentCommand = ReactiveCommand.Create(CallCreateAppointment);
        }
        #endregion

        #region Init Methods
        private void InitViewModels()
        {
            _messageViewModel = new MessageViewModel();
            _patientViewModel = new PatientViewModel();
            _doctorViewModel = new DoctorViewModel();
            _appointmentTimeViewModel = new AppointmentTimeViewModel();
        }

        private void InitModels()
        {
            _hospitalModel = new HospitalModel();
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

        private void InitAndSubscribeUpdateListTakenTimesDoctor()
        {
            var combinedPropertiesUpdateCanCreate = this.WhenAnyValue(
               x => x._doctorViewModel.SelectedDoctor);

            combinedPropertiesUpdateCanCreate
                .Subscribe(_ => UpdateListFreeTimesDoctor());
        }
        #endregion

        private void CallCreateAppointment()
        {
            if (!PatientExist())
            {
                CreatePatient();
                CreateAppointment();

                ShowMessageView($"Вы записаны к {DoctorViewModel.SelectedDoctor}\n" +
                    $"на время\n" +
                    $"{AppointmentTimeViewModel.SelectedAppointmentTimeModel}", "Успешно");
            }
            else
            {
                ShowMessageView("Пациент с таким именем и фамилией уже существует", "Ошибка");
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

        private void UpdateListFreeTimesDoctor()
        {
            if (_doctorViewModel.SelectedDoctor != null)
            {
                _doctorViewModel.ListFreeTimesDoctor = new ObservableCollection<AppointmentTimeModel>
                    (_hospitalModel.GetListFreeTimesDoctor(_doctorViewModel.SelectedDoctor));
            }
            else
            {
                _doctorViewModel.ListFreeTimesDoctor = new ObservableCollection<AppointmentTimeModel>();
            }
        }

        private void UpdateCanCreateAppointment()
        {
            CanCreateAppointment = _appointmentTimeViewModel.IsCanCreateAppointment()
                && _doctorViewModel.IsCanCreateAppointment()
                && _patientViewModel.IsCanCreateAppointment();
        }

        private void ClearInputFieldsAndSelections()
        {
            _patientViewModel.ClearFields();
            _doctorViewModel.ClearFields();
            _appointmentTimeViewModel.ClearFields();
        }

        private void ShowMessageView(string message, string type)
        {
            _messageViewModel.ShowMessage(message, type);
        }
    }
}
