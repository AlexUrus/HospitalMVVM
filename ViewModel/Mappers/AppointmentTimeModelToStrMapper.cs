using Model.Model;

namespace ViewModel.Mappers
{
    public class AppointmentTimeModelToStrMapper : IMapperModelToStr<AppointmentTimeModel>
    {
        private Dictionary<string, AppointmentTimeModel> keyValuePairs = new Dictionary<string, AppointmentTimeModel>();

        public string ModelToString(AppointmentTimeModel model)
        {
            string str = $"с {model.StartTime:hh\\:mm} по {model.EndTime:hh\\:mm}";
            keyValuePairs.Add(str, model);
            return str;
        }

        public AppointmentTimeModel? StringToModel(string str)
        {
            keyValuePairs.TryGetValue(str, out AppointmentTimeModel appointmentTime);

            if (appointmentTime != null)
                keyValuePairs.Remove(str);
            return appointmentTime;
        }
    }
}
