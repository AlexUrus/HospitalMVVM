using Model.Model;
using ReactiveUI;

namespace ViewModel
{
    public class PatientViewModel : ReactiveObject
    {
        //private PatientModel? _patientModel { get; set; }
        private string? _patientName;
        private string? _patientSurname;
        public string? PatientName
        {
            get => _patientName;
            set => this.RaiseAndSetIfChanged(ref _patientName, value);
        }

        public string? PatientSurname
        {
            get => _patientSurname;
            set => this.RaiseAndSetIfChanged(ref _patientSurname, value);
        }

    }
}
