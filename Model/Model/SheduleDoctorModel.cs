

namespace Model.Model
{
    public class SheduleDoctorModel
    {
        public int Id { get; private set; }
        public DoctorModel Doctor { get; private set; }
        public List<AppointmentModel> Appointments { get; private set; }
        public List<AppointmentTimeModel> WorkSheduleTime { get; private set; }

        public SheduleDoctorModel(int id, DoctorModel doctorModel, List<AppointmentModel> appointments, List<AppointmentTimeModel> workSheduleTime)
        {
            Id = id;
            Doctor = doctorModel;
            Appointments = appointments;
            WorkSheduleTime = workSheduleTime;
        }



    }
}
