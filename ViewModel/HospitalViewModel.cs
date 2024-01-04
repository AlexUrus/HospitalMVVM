using Model.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using Mappers;
using ViewModel.Models;

namespace ViewModel
{
    public class HospitalViewModel : BaseViewModel
    {
        #region Fields
        private HospitalModel _hospitalModel;
        private PatientViewModel _patientViewModel;
        private DoctorViewModel _doctorViewModel;
        private AppointmentTimeViewModel _appointmentTimeViewModel;
        private DoctorSheduleViewModel _doctorSheduleViewModel;

        private MessageViewModel _messageViewModel;

        private bool _canCreateAppointment;

        private string _selectedDoctor;
        private string _selectedAppointmetTime;

        private DoctorModelToStrMapper _doctorModelToStrMapper;
        private AppointmentTimeModelToStrMapper _appointmentTimeModelToStrMapper;
        private TypeDoctorModelToStrMapper _typeDoctorModelToStrMapper;

        private ObservableCollection<string> _doctorModels;
        private ObservableCollection<string> _appointmentTimeModels;
        #endregion

        #region Properties

        public ObservableCollection<string> DoctorModels
        {
            get => _doctorModels;
            private set => this.RaiseAndSetIfChanged(ref _doctorModels, value);
        }
        public ObservableCollection<string> AppointmentTimeModels
        {
            get => _appointmentTimeModels;
            private set => this.RaiseAndSetIfChanged(ref _appointmentTimeModels, value);
        }
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
        public DoctorSheduleViewModel DoctorSheduleViewModel
        {
            get => _doctorSheduleViewModel;
            private set => this.RaiseAndSetIfChanged(ref _doctorSheduleViewModel, value);
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

        #region Constructor
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
            _doctorSheduleViewModel = new DoctorSheduleViewModel();
        }

        private void InitModels(HospitalModel hospitalModel)
        {
            _hospitalModel = hospitalModel;
            AppointmentTimeModels = new ObservableCollection<string>(Mapper.ConvertModelListToString(_hospitalModel.AppointmentTimeModels));
            DoctorModels = new ObservableCollection<string>(Mapper.ConvertModelListToString(_hospitalModel.DoctorModels));
        }

        private void InitMappers()
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
            var doctorChanges = this.WhenAnyValue(x => x._doctorViewModel.Doctor);

            doctorChanges.Subscribe(_ => UpdateListFreeTimesDoctor());
        }
        #endregion

        public List<DoctorSheduleTableField> GetSheduleDoctors()
        {
            List<DoctorSheduleTableField> sheduleTableFields = new List<DoctorSheduleTableField>();
            var doctorShedules = _hospitalModel.GetSheduleDoctors();
            foreach (var item in doctorShedules)
            {
                sheduleTableFields.Add(new DoctorSheduleTableField()
                {
                    Часы_приема = item.AppointmentTime,
                    Id = item.Id,
                    Фамилия = item.Surname,
                    Имя = item.Name,
                });
            }

            return sheduleTableFields;
        }

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
                AppointmentTimeModels = new ObservableCollection<string>(Mapper.ConvertModelListToString(_doctorViewModel.ListFreeTimesDoctor));
            }
            else
            {
                _doctorViewModel.ListFreeTimesDoctor = new ObservableCollection<AppointmentTimeModel>();
                AppointmentTimeModels = new ObservableCollection<string>(Mapper.ConvertModelListToString(_doctorViewModel.ListFreeTimesDoctor));
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
