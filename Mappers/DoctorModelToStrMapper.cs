using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers
{
    public class DoctorModelToStrMapper: IMapperModelToStr<DoctorModel>
    {
        private static Dictionary<string,DoctorModel> keyValuePairs = new Dictionary<string,DoctorModel>();

        public string ModelToString(DoctorModel model)
        {
            string str = $"{model.Name} {model.Surname} {model.Type.Type}";
            if (!keyValuePairs.ContainsKey(str))
            {
                keyValuePairs.Add(str, model);
            }
            return str;
        }

        public DoctorModel? StringToModel(string str) 
        {
            keyValuePairs.TryGetValue(str, out DoctorModel doctor);

            if(doctor != null) 
                keyValuePairs.Remove(str);
            return doctor;
        }
    }
}
