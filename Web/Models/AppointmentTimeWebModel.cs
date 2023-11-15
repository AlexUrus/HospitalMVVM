namespace Web.Models
{
    public class AppointmentTimeWebModel
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
