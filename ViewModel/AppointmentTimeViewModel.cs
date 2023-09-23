using DynamicData;
using Model.Model;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.EFModel;

namespace ViewModel
{
    public class AppointmentTimeViewModel : ReactiveObject
    {
        private AppointmentTimeModel? _selectedAppointmentTimeModel;

        public AppointmentTimeModel? SelectedAppointmentTimeModel
        {
            get => _selectedAppointmentTimeModel;
            set => this.RaiseAndSetIfChanged(ref _selectedAppointmentTimeModel, value);
        }
        
    }

}
