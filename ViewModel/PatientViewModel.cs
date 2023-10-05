using Model.Model;
using ReactiveUI;

namespace ViewModel
{
    public class PatientViewModel : ReactiveObject
    {
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

        public void ClearFields()
        {
            PatientName = null;
            PatientSurname = null;
        }

        public bool IsCanCreateAppointment()
        {
            return !string.IsNullOrWhiteSpace(PatientName) && !string.IsNullOrWhiteSpace(PatientSurname);
        }

    }
}
