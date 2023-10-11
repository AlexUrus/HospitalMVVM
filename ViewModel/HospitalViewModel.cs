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
            AppointmentTimeModels = new ObservableCollection<string>(ConvertModelListToString(_hospitalModel.AppointmentTimeModels));
            DoctorModels = new ObservableCollection<string>(ConvertModelListToString(_hospitalModel.DoctorModels));
        }

        private void InitAndSubscribeUpdateCanCreate()
        {
            var combinedPropertiesUpdateCanCreate = this.WhenAnyValue(
               x => x._patientViewModel.PatientName,
               x => x._patientViewModel.PatientSurname,
               x => x._doctorViewModel.Doctor,
               x => x._appointmentTimeViewModel.SelectedAppointmentTimeModel);

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
            AppointmentTimeModel appointmentTimeModel = _appointmentTimeViewModel.SelectedAppointmentTimeModel;
            PatientModel? patientModel = _hospitalModel.CreatePatient(_patientViewModel.PatientName,_patientViewModel.PatientSurname);
            
            string result = _hospitalModel.CallCreateAppointment(patientModel, doctorModel, appointmentTimeModel);

            if (result == "Ok")
            { 
                ShowMessageView($"Вы записаны к {DoctorViewModel.Doctor}\n" +
                    $"на время\n" +
                    $"{AppointmentTimeViewModel.SelectedAppointmentTimeModel}", "Успешно");
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

        private ICollection<string> ConvertModelListToString<T>(ICollection<T> models) where T : AbstractModel
        {
            List<string> listDoctorModelStrings = new List<string>();

            foreach (var model in models)
            {
                listDoctorModelStrings.Add(ModelToString(model));
            }

            return listDoctorModelStrings;
        }

        private string ModelToString(AbstractModel model)
        {
            if (model is DoctorModel doctor)
            {
                return $"{doctor.Name} {doctor.Surname} {doctor.Type}";
            }
            if(model is AppointmentTimeModel appointmentTime)
            {
                return $"с {appointmentTime.StartTime:hh\\:mm} по {appointmentTime.EndTime:hh\\:mm}";
            }
            return "Unknown";
        }
    }
}
