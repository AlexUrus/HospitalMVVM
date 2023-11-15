using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class DoctorRepo : IDoctorRepo
    {
        private readonly HospitalContext _context;
        public DoctorRepo(HospitalContext context)
        {
            _context = context;
        }

        public ICollection<DoctorModel> GetDoctorModels()
        {
            ICollection<DoctorModel> doctorModels;
            try
            {
                ICollection<Doctor> listDoctor = _context.Doctors.ToList();
                doctorModels = new List<DoctorModel>();

                foreach (Doctor doctor in listDoctor)
                {
                    doctorModels.Add(new DoctorModel(doctor.Id, doctor.Name, doctor.Surname, new TypeDoctorModel(doctor.Type.Id, doctor.Type.Type)));
                }

                return doctorModels;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InitDoctors(ICollection<TypeDoctorModel> typeDoctorModels)
        {
            try
            {
                ICollection<TypeDoctor> typeDoctors = new List<TypeDoctor>();

                foreach (var model in typeDoctorModels)
                {
                    typeDoctors.Add(ConverterModelToEF.Convert(model));
                }

                ICollection<Doctor> doctors = new List<Doctor>()
                {
                    new Doctor()
                    {
                        Name = "Иван",
                        Surname = "Иванов",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Терапевт")
                    },
                    new Doctor()
                    {
                        Name = "Михаил",
                        Surname = "Михайлов",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Дерматолог")
                    },
                    new Doctor()
                    {
                        Name = "Петр",
                        Surname = "Петров",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "ЛОР")
                    },
                    new Doctor()
                    {
                        Name = "Сидр",
                        Surname = "Сидоров",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Психолог")
                    },
                    new Doctor()
                    {
                        Name = "Александр",
                        Surname = "Александров",
                        Type = typeDoctors.FirstOrDefault(t => t.Type == "Гастроэнтеролог")
                    }
                };

                _context.Doctors.AddRange(doctors);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
