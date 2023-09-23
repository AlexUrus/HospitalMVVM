using Data;
using Model.EFModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class AppointmentTimeModel 
    {
        public int Id { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public AppointmentTimeModel(int id, TimeSpan startTime, TimeSpan endTime) 
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString()
        {
            return $"с {StartTime.ToString(@"hh\:mm")} по {EndTime.ToString(@"hh\:mm")}";
        }
    }
}
