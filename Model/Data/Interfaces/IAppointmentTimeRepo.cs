using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.Interfaces
{
    public interface IAppointmentTimeRepo
    {
        public void InitAppointmentTimes();
        public ICollection<AppointmentTimeModel> GetAppointmentTimes();
    }
}
