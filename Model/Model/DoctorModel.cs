using Model.Data;
using Model.EFModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class DoctorModel 
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public TypeDoctorModel Type { get; private set; }

        public DoctorModel(int id, string name, string surname, TypeDoctorModel type) 
        {
            Id = id;
            Name = name;
            Surname = surname;
            Type = type;
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {Type}";
        }
    }
}
