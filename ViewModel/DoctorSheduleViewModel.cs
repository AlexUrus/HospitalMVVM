using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace ViewModel
{
    public class DoctorSheduleViewModel : BaseViewModel
    {
        public ObservableCollection<DoctorSheduleTableField> DoctorSheduleTableFields { get; set; }
        
        public DoctorSheduleViewModel()
        {
        }

        private void InitDoctorSheduleModels()
        {
            DoctorSheduleTableFields = 
        }
    }
}
