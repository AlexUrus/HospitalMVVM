using Microsoft.Data.SqlClient;
using Model.Data.Interfaces;
using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly HospitalContext _context;
        public PatientRepo(HospitalContext context)
        {
            _context = context;
        }

        public void CreatePatient(string name, string surname)
        {
            try
            {
                Patient patient = new Patient() { Name = name, Surname = surname };
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public PatientModel GetPatientModel(string name, string surname)
        {
            try
            {
                Patient patient = _context.Patients.FirstOrDefault(p => p.Name == name && p.Surname == surname);
                return new PatientModel(patient.Id, patient.Name, patient.Surname);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool PatientExists(string name, string surname)
        {
            try
            {
                bool exists = _context.Patients.Any(p => p.Name == name && p.Surname == surname);
                return exists;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
