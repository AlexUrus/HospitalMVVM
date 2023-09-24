using Model.Data;
using Model.EFModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class DoctorModel 
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public TypeDoctorModel Type { get; private set; }

        private ObservableCollection<AppointmentTimeModel> _listTakenTimesDoctor;

        public ObservableCollection<AppointmentTimeModel> ListFreeTimesDoctor {
            get
            {
                return DataRepository.Instance.GetListFreeTimesDoctor(this);
            }
        }

        public DoctorModel(int id, string name, string surname, TypeDoctorModel type) 
        {
            Id = id;
            Name = name;
            Surname = surname;
            Type = type;
        }

        public bool IsFreeDoctorInTime(AppointmentTimeModel appointmentTimeModel)
        {
            return DataRepository.Instance.IsFreeDoctorInTime(appointmentTimeModel, this);
        }

        public ObservableCollection<AppointmentTimeModel> GetFreeTimeDoctor()
        {
            return DataRepository.Instance.GetListFreeTimesDoctor(this);
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {Type}";
        }
    }
}
