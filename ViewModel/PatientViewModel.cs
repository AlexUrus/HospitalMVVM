using Model.Model;
using ReactiveUI;

namespace ViewModel
{
    public class PatientViewModel : ReactiveObject
    {
        private PatientModel _patientModel { get; set; }
        public string? PatientName
        {
            get => _patientModel.Name;
            set => this.RaisePropertyChanged(nameof(PatientName));
        }

        public string? PatientSurname
        {
            get => _patientModel.Surname;
            set => this.RaisePropertyChanged(nameof(PatientSurname));
        }

        public void CreatePatient()
        {
            _patientModel.
        }

    }
}
