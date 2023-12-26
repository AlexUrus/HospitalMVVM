using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers
{
    public static class Mapper
    {
        private static DoctorModelToStrMapper _doctorModelToStrMapper = new DoctorModelToStrMapper();
        private static AppointmentTimeModelToStrMapper _appointmentTimeModelToStrMapper = new AppointmentTimeModelToStrMapper();
        private static TypeDoctorModelToStrMapper _typeDoctorModelToStrMapper = new TypeDoctorModelToStrMapper();

        public static string ModelToString(object model)
        {
            if (model is DoctorModel doctor)
            {
                return _doctorModelToStrMapper.ModelToString(doctor);
            }
            if (model is AppointmentTimeModel appointmentTime)
            {
                return _appointmentTimeModelToStrMapper.ModelToString(appointmentTime);
            }
            if (model is TypeDoctorModel typeDoctor)
            {
                return _typeDoctorModelToStrMapper.ModelToString(typeDoctor);
            }
            return "Unknown";
        }

        public static ICollection<string> ConvertModelListToString<Model>(ICollection<Model> models) where Model : class
        {
            List<string> listDoctorModelStrings = new List<string>();

            foreach (var model in models)
            {
                listDoctorModelStrings.Add(ModelToString(model));
            }

            return listDoctorModelStrings;
        }
    }
}
