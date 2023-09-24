
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
