
namespace Model.Model
{
    public class AppointmentTimeModel : AbstractModel
    {
        public int Id { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public AppointmentTimeModel(int id, TimeSpan startTime, TimeSpan endTime) 
        {
            Id = id;
            if (endTime < startTime) throw new ArgumentNullException("Невозможно создать время записи");
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}
