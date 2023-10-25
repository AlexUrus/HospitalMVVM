using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public interface IRepository
    {
        public ICollection<TypeDoctorModel> GetTypeDoctorModels();
        public ICollection<DoctorModel> GetDoctorModels();
        public ICollection<AppointmentTimeModel> GetAppointmentTimes();
        public void InitTypeDoctorModels();
        public void InitDoctors();
        public void InitAppointmentTimes();
    }
}
