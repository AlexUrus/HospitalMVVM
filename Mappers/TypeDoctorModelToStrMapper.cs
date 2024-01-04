using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers
{
    public class TypeDoctorModelToStrMapper : IMapperModelToStr<TypeDoctorModel>
    {
        private static Dictionary<string, TypeDoctorModel> keyValuePairs = new Dictionary<string, TypeDoctorModel>();

        public string ModelToString(TypeDoctorModel model)
        {
            string str = model.Type;
            if (!keyValuePairs.ContainsKey(str))
            {
                keyValuePairs.Add(str, model);
            }
            return str;
        }

        public TypeDoctorModel? StringToModel(string str)
        {
            keyValuePairs.TryGetValue(str, out TypeDoctorModel doctorModel);

            if (doctorModel != null)
                keyValuePairs.Remove(str);
            return doctorModel;
        }
    }
}
