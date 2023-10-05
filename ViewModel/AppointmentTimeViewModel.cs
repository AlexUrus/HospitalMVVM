﻿using Model.Model;
using ReactiveUI;


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

        public void ClearFields()
        {
            SelectedAppointmentTimeModel = null;
        }
        public bool IsCanCreateAppointment()
        {
            return SelectedAppointmentTimeModel != null;
        }
    }
}
