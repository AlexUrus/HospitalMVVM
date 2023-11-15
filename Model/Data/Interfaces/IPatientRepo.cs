using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.Interfaces
{
    public interface IPatientRepo
    {
        public void CreatePatient(string name, string surname);
        public bool PatientExists(string name, string surname);
        public PatientModel GetPatientModel(string name, string surname);
    }
}
