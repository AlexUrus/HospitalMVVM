using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class TypeDoctorModel 
    {
        public int Id { get; init; }
        public string Type { get; init; }
        
        public TypeDoctorModel(int id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}
