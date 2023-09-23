using Model.Data;
using Model.EFModel;
using System;
using System.Collections.Generic;
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
        public TypeDoctor Type { get; private set; }

        public DoctorModel(int id, string name, string surname, TypeDoctor type) 
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

        public List<AppointmentTimeModel> GetFreeTimeDoctor()
        {
            return DataRepository.Instance.GetListFreeTimesDoctor(this);
        }
    }
}
