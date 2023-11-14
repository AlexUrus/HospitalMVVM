

namespace Web.Models
{
    public class AppointmentWebModel
    {
        public int Id { get; set; }
        public DoctorWebModel Doctor { get; set; }
        public PatientWebModel Patient { get; set; }
        public AppointmentTimeWebModel AppointmentTime { get; set; }
    }
}
