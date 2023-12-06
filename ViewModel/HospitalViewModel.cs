using Model.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using Mappers;

namespace ViewModel
{
    public class HospitalViewModel : BaseViewModel
    {
        #region Fields
        private HospitalModel _hospitalModel;
        private PatientViewModel _patientViewModel;
        private DoctorViewModel _doctorViewModel;
        private AppointmentTimeViewModel _appointmentTimeViewModel;

        private MessageViewModel _messageViewModel;

        private bool _canCreateAppointment;

        private string _selectedDoctor;
        private string _selectedAppointmetTime;

        private DoctorModelToStrMapper _doctorModelToStrMapper;
        private AppointmentTimeModelToStrMapper _appointmentTimeModelToStrMapper;
        private TypeDoctorModelToStrMapper _typeDoctorModelToStrMapper;

        #endregion

        #region Properties
        public ObservableCollection<string> DoctorModels { get; set; }
        public ObservableCollection<string> AppointmentTimeModels { get; set; }
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

        public string SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedDoctor, value);
                if (value != null)
                    _doctorViewModel.Doctor = _doctorModelToStrMapper.StringToModel( _selectedDoctor);
            }
        }

        public string SelectedAppointmetTime
        {
            get => _selectedAppointmetTime;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedAppointmetTime, value);
                if(value != null)
                    _appointmentTimeViewModel.AppointmentTimeModel = _appointmentTimeModelToStrMapper.StringToModel(_selectedAppointmetTime);
            }
        }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, Unit> CreateAppointmentCommand { get; }
        #endregion

        #region Constructors
        public HospitalViewModel(HospitalModel hospitalModel)
        {
            InitViewModels();
            InitMappers();
            InitModels(hospitalModel);
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

        private void InitModels(HospitalModel hospitalModel)
        {
            _hospitalModel = hospitalModel;
            AppointmentTimeModels = new ObservableCollection<string>(ConvertModelListToString(_hospitalModel.AppointmentTimeModels));
            DoctorModels = new ObservableCollection<string>(ConvertModelListToString(_hospitalModel.DoctorModels));
        }

        public void InitMappers()
        {
            _appointmentTimeModelToStrMapper = new AppointmentTimeModelToStrMapper();
            _doctorModelToStrMapper = new DoctorModelToStrMapper();
        }

        private void InitAndSubscribeUpdateCanCreate()
        {
            var combinedPropertiesUpdateCanCreate = this.WhenAnyValue(
               x => x._patientViewModel.PatientName,
               x => x._patientViewModel.PatientSurname,
               x => x._doctorViewModel.Doctor,
               x => x._appointmentTimeViewModel.AppointmentTimeModel);

            combinedPropertiesUpdateCanCreate
                .Subscribe(_ => UpdateCanCreateAppointment());
        }

        private void InitAndSubscribeUpdateListTakenTimesDoctor()
        {
            var combinedPropertiesUpdateCanCreate = this.WhenAnyValue(
               x => x._doctorViewModel.Doctor);

            combinedPropertiesUpdateCanCreate
                .Subscribe(_ => UpdateListFreeTimesDoctor());
        }
        #endregion

        private void CallCreateAppointment()
        {
            DoctorModel doctorModel = _doctorViewModel.Doctor;
            AppointmentTimeModel appointmentTimeModel = _appointmentTimeViewModel.AppointmentTimeModel;
            PatientModel? patientModel = _hospitalModel.CreatePatient(_patientViewModel.PatientName, _patientViewModel.PatientSurname);

            string result = _hospitalModel.CallCreateAppointment(patientModel, doctorModel, appointmentTimeModel);

            if (result == "OK")
            {
                ShowMessageView($"Вы записаны к {SelectedDoctor}\n" +
                    $"на время\n" +
                    $"{SelectedAppointmetTime}", "Успешно");
            }
            else
            {
                ShowMessageView("Пациент с таким именем и фамилией уже существует", "Ошибка");
            }
            ClearInputFieldsAndSelections();
        }

        private void UpdateListFreeTimesDoctor()
        {
            if (_doctorViewModel.Doctor != null)
            {
                _doctorViewModel.ListFreeTimesDoctor = new ObservableCollection<AppointmentTimeModel>
                    (_hospitalModel.GetListFreeTimesDoctor(_doctorViewModel.Doctor));
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

        private ICollection<string> ConvertModelListToString<Model>(ICollection<Model> models) where Model: class
        {
            List<string> listDoctorModelStrings = new List<string>();

            foreach (var model in models)
            {
                listDoctorModelStrings.Add(ModelToString(model));
            }

            return listDoctorModelStrings;
        }

        private string ModelToString(object model)
        {
            if (model is DoctorModel doctor)
            {
                return _doctorModelToStrMapper.ModelToString(doctor);
            }
            if (model is AppointmentTimeModel appointmentTime)
            {
                return _appointmentTimeModelToStrMapper.ModelToString(appointmentTime);
            }
            if(model is TypeDoctorModel typeDoctor)
            {
                return _typeDoctorModelToStrMapper.ModelToString(typeDoctor);
            }
            return "Unknown";
        }
    }
}
