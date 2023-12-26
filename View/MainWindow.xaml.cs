using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool _isVolumeOn = true;

        public bool IsVolumeOn
        {
            get { return _isVolumeOn; }
            set
            {
                if (_isVolumeOn != value)
                {
                    _isVolumeOn = value;
                    OnPropertyChanged(nameof(IsVolumeOn));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainWindow()
        {
            InitializeComponent();
            AudioElement.Play();
        }

        private void VolumeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (IsVolumeOn) AudioElement.Pause(); 
            else AudioElement.Play();

            IsVolumeOn = !IsVolumeOn;
        }

        private void DoctorsShedule_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var doctorsSheduleWindow = new DoctorsSheduleWindow();
            var doctorSheduleViewModel = new DoctorSheduleViewModel();
            doctorsSheduleWindow.DataContext = doctorSheduleViewModel;
            doctorsSheduleWindow.Show();
        }
    }
}
