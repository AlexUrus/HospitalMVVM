using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.Interfaces
{
    public interface IDoctorRepo
    {
        public ICollection<DoctorModel> GetDoctorModels();
        public void InitDoctors(ICollection<TypeDoctorModel> typeDoctorModels);
    }
}
